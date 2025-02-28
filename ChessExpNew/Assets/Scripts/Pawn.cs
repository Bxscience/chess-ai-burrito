using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isSelected = false;
    private float moveSpeed = 10f;
    private Vector3 originalPosition;
    private bool hasMovedBefore = false;
    private bool isWhite;
    public GameObject GameManager;

    void Start()
    {
        targetPosition = transform.position;
        originalPosition = transform.position;
        // Determine if pawn is white based on starting position
        isWhite = transform.position.z < 0; 
        Debug.Log($"Pawn initialized at position: {originalPosition}, isWhite: {isWhite}");
        GameManager = GameObject.Find("GameManager");
        DeselectPiece(); 
    }

/*
    void Update()
    {
        // Move the piece to target position
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!isSelected)
                    {
                        SelectPiece();
                    }
                }
                else if (isSelected)
                {
                    if (hit.collider.CompareTag("ChessSquare"))
                    {
                        if (IsValidPawnMove(hit.point))
                        {
                            MovePiece(hit.point);
                            hasMovedBefore = true;
                        }
                        else
                        {
                            Debug.Log($"Invalid pawn move to {hit.point}. Current pos: {transform.position}, IsWhite: {isWhite}");
                            DeselectPiece();
                        }
                    }
                    else
                    {
                        DeselectPiece();
                    }
                }
            }
        }
    }
*/
    void Update() {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        if (GameManager.GetComponent<GameManager>().player1_turn == true && isWhite == true || GameManager.GetComponent<GameManager>().player2_turn == true && isWhite == false) {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        if (!isSelected)
                        {
                            SelectPiece();
                        }
                    }
                    else if (isSelected)
                    {
                        if (hit.collider.CompareTag("ChessSquare"))
                        {
                            if (IsValidPawnMove(hit.point))
                            {
                                GameObject temp = GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)];
                                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)] = null;
                                GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)] = temp;
                                MovePiece(hit.point);
                                hasMovedBefore = true;
                                GameManager.GetComponent<GameManager>().player1_turn = false;
                                GameManager.GetComponent<GameManager>().player2_turn = false;
                                if (isWhite == true)
                                    GameManager.GetComponent<GameManager>().P2Turn();
                                else
                                    GameManager.GetComponent<GameManager>().P1Turn();
                            }
                            else
                            {
                                Debug.Log($"Invalid pawn move to {hit.point}. Current pos: {transform.position}, IsWhite: {isWhite}");
                                DeselectPiece();
                            }
                        }
                        else
                        {
                            DeselectPiece();
                        }
                    }
                }
            }
        }
    }

    bool IsValidPawnMove(Vector3 newPosition)
    {
        // Calculate the movement vector from current position
        Vector3 currentPos = new Vector3(
            Mathf.Floor(transform.position.x) + 0.5f,
            originalPosition.y,
            Mathf.Floor(transform.position.z) + 0.5f
        );

        Vector3 targetPos = new Vector3(
            Mathf.Floor(newPosition.x) + 0.5f,
            originalPosition.y,
            Mathf.Floor(newPosition.z) + 0.5f
        );

        Vector3 movement = targetPos - currentPos;

        // Direction multiplier (1 for white moving up, -1 for black moving down)
        float directionMultiplier = isWhite ? 1 : -1;

        // Check if moving straight 
        if (Mathf.Abs(movement.x) > 0.1f)
        {
            Debug.Log($"Invalid horizontal movement: {movement.x}");
            return false;
        }

        // Get forward movement in the correct direction
        float forwardMovement = movement.z * directionMultiplier;

        Debug.Log($"Forward movement: {forwardMovement}, HasMovedBefore: {hasMovedBefore}");

        // First move can be 1 or 2 squares
        if (!hasMovedBefore)
        {
            return forwardMovement > 0 && forwardMovement <= 2;
        }

        // Subsequent moves can only be 1 square
        return forwardMovement > 0 && forwardMovement <= 1;
    }

    void SelectPiece()
    {
        isSelected = true;
        List<Move> temp = getAllMoves();
        for (int x = 0; x < temp.Count; x++) {
            GameManager.GetComponent<GameManager>().blocks[temp[x].endz, temp[x].endx].GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log(temp[x].endz + " " + temp[x].endx);
        }
        GetComponent<Renderer>().material.color = Color.yellow;
        targetPosition = transform.position + Vector3.up * 0.5f;
    }

    void DeselectPiece()
    {
        isSelected = false;
        for (int z = 0; z < 8; z++)
            for (int x = 0; x < 8; x++) {
                GameObject temp = GameManager.GetComponent<GameManager>().blocks[z, x];
                if ((z+x) % 2 == 0)
                    temp.GetComponent<Renderer>().material.color = Color.white;
                else
                    temp.GetComponent<Renderer>().material.color = Color.black;
            }

        if (isWhite)
            GetComponent<Renderer>().material.color = Color.white;
        else
            GetComponent<Renderer>().material.color = Color.grey;
        targetPosition = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
    }

    void MovePiece(Vector3 newPosition)
    {
        transform.position = new Vector3(
            Mathf.Floor(newPosition.x) + 0.5f,
            originalPosition.y,
            Mathf.Floor(newPosition.z) + 0.5f
        );
        DeselectPiece();
    }

    public List<Move> getAllMoves() {
        List<Move> moves = new List<Move>();
        if (isWhite) {
            if (transform.position.z + 3.5f < 7)
                if (GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5)));
            if (hasMovedBefore == false && GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5)] == null
            && GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 5.5f), (int) (transform.position.x + 3.5)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 5.5f), (int) (transform.position.x + 3.5)));
            if (transform.position.x + 3.5f > 0 && transform.position.x < 7) {
                if (GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)] != null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5)));
                if (GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)] != null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5)));
            }
        }
        else {
            if (transform.position.z + 3.5f > 0)
                if (GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5)));
            if (hasMovedBefore == false && GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5)] == null
            && GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 1.5f), (int) (transform.position.x + 3.5)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 1.5f), (int) (transform.position.x + 3.5)));
            if (transform.position.x + 3.5f < 7)
                if (GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)] != null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5)));
            if (transform.position.x + 3.5f > 0)    
                if (GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)] != null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5)));
        }
        return moves;
    }
    
}