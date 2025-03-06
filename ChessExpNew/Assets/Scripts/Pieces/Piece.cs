using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour // Main Piece class
// handles all behaviors essential for selecting a piece, deselecting, and highlighting the board
{
    public Vector3 targetPosition;
    public bool isSelected = false;
    public float moveSpeed = 10f;
    public Vector3 originalPosition;
    public bool isWhite;
    public GameObject GameManager;
    public bool hasMovedBefore;
    public bool alive;

    void Start()
    {
        targetPosition = transform.position;
        originalPosition = transform.position;
        // Determine if pawn is white based on starting position
        isWhite = transform.position.z < 0; 
        Debug.Log($"Pawn initialized at position: {originalPosition}, isWhite: {isWhite}");
        GameManager = GameObject.Find("GameManager");
        alive = true;
        DeselectPiece();
    }
    void Update() {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        
        if ((GameManager.GetComponent<GameManager>().player1_turn == true && isWhite == true || GameManager.GetComponent<GameManager>().player2_turn == true && isWhite == false) && alive == true) {
            if (GameManager.GetComponent<GameManager>().p1_check == false && GameManager.GetComponent<GameManager>().player1_turn == true || GameManager.GetComponent<GameManager>().player1_turn == true && 
            GameManager.GetComponent<GameManager>().p1_check == true && GetComponent<King>() != null || 
            GameManager.GetComponent<GameManager>().p2_check == false && GameManager.GetComponent<GameManager>().player2_turn == true || GameManager.GetComponent<GameManager>().player2_turn == true && 
            GameManager.GetComponent<GameManager>().p2_check == true && GetComponent<King>()) { // checking if it's your turn, and can move // you cannot move anything besides the king in check
                GetComponent<Collider>().enabled = true;
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
                                Highlight();
                            }
                        }
                        else if (isSelected)
                        {
                            if (hit.collider.CompareTag("ChessSquare"))
                            {
                                if (hit.collider.GetComponent<Renderer>().material.color == Color.yellow) // if yellow, switch, and make sure dead pieces are stored
                                {
                                    GameObject temp = GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)];
                                    GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)] = null;
                                    if (GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)] != null) {
                                        GameObject temp1 = GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)];
                                        temp1.GetComponent<Piece>().alive = false;
                                        temp1.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                                        GameManager.GetComponent<GameManager>().dead_pieces.Add(temp1);
                                    }
                                    GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)] = temp;
                                    MovePiece(hit.point);
                                    hasMovedBefore = true;
                                    GameManager.GetComponent<GameManager>().player1_turn = false;
                                    GameManager.GetComponent<GameManager>().player2_turn = false;
                                    GameManager.GetComponent<GameManager>().p1_check = false;
                                    GameManager.GetComponent<GameManager>().p2_check = false;
                                    if (isWhite == true)
                                        GameManager.GetComponent<GameManager>().P2Turn();
                                    else
                                        GameManager.GetComponent<GameManager>().P1Turn();
                                    UnHighlight();
                                }
                                else
                                {
                                    Debug.Log($"Invalid move to {hit.point}. Current pos: {transform.position}, IsWhite: {isWhite}");
                                    DeselectPiece();
                                    UnHighlight();
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
        else
            GetComponent<Collider>().enabled = false;
    }

    void SelectPiece() // select the piece
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        targetPosition = transform.position + Vector3.up * 0.5f;
    }

    void DeselectPiece() // bring it back down
    {
        isSelected = false;
        if (isWhite)
            GetComponent<Renderer>().material.color = Color.white;
        else
            GetComponent<Renderer>().material.color = Color.grey;
        targetPosition = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
    }

    void Highlight() { // how everything is highlighted
        for (int z = 0; z < 8; z++)
            for (int x = 0; x < 8; x++) {
                GameObject temp1 = GameManager.GetComponent<GameManager>().blocks[z, x];
                if ((z+x) % 2 == 0)
                    temp1.GetComponent<Renderer>().material.color = Color.white;
                else
                    temp1.GetComponent<Renderer>().material.color = Color.black;
            }
        List<Move> temp = getAllMoves(GameManager.GetComponent<GameManager>().game);
        for (int x = 0; x < temp.Count; x++) {
            GameManager.GetComponent<GameManager>().blocks[temp[x].endz, temp[x].endx].GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log(temp[x].endz + " " + temp[x].endx);
        }
    }

    void UnHighlight() { // removes the highlight
        for (int z = 0; z < 8; z++)
            for (int x = 0; x < 8; x++) {
                GameObject temp = GameManager.GetComponent<GameManager>().blocks[z, x];
                if ((z+x) % 2 == 0)
                    temp.GetComponent<Renderer>().material.color = Color.white;
                else
                    temp.GetComponent<Renderer>().material.color = Color.black;
            }
    }

    public void MovePiece(Vector3 newPosition) // how switching places is handled
    {
        transform.position = new Vector3(
            Mathf.Floor(newPosition.x) + 0.5f,
            originalPosition.y,
            Mathf.Floor(newPosition.z) + 0.5f
        );
        DeselectPiece();
    }
    public virtual List<Move> getAllMoves(GameObject[,] arr) { // all pieces override this method and add their unique ways of moving
        return new List<Move>();
    }
}