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
    public bool en_passant = false;
    public bool promote = false;

    void Start()
    {
        targetPosition = transform.position;
        originalPosition = transform.position;
        // Determine if pawn is white based on starting position
        if (gameObject.name == "PawnLight(Clone)" || gameObject.name == "KnightLight(Clone)" || gameObject.name == "RookLight(Clone)" || gameObject.name == "BishopLight(Clone)" || gameObject.name == "QueenLight(Clone)" || gameObject.name == "KingLight(Clone)")
            isWhite = true;
        else
            isWhite = false;
        GameManager = GameObject.Find("GameManager");
        alive = true;
        DeselectPiece();
    }
    public void Summon(bool color)
    {
        targetPosition = transform.position;
        originalPosition = transform.position;
        // Determine if pawn is white based on starting position
        isWhite = color;
        GameManager = GameObject.Find("GameManager");
        alive = true;
        DeselectPiece();
    }
    void Update() {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        
        if ((GameManager.GetComponent<GameManager>().player1_turn == true && isWhite == true && GameManager.GetComponent<GameManager>().p_one_AI == false || GameManager.GetComponent<GameManager>().player2_turn == true && isWhite == false && GameManager.GetComponent<GameManager>().p_two_AI == false) && alive == true) {
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
                                
                                if (temp.GetComponent<Pawn>() != null &&
                                GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)] == null
                                && (int) (Mathf.Floor(hit.point.z) + 4) != (int) (transform.position.z + 3.5)
                                && (int) (Mathf.Floor(hit.point.x) + 4) != (int) (transform.position.x + 3.5)) {
                                    GameObject temp0;
                                    if ((int) (Mathf.Floor(hit.point.z) + 4) > (int) (transform.position.z + 3.5))
                                        temp0 = GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 3), (int) (Mathf.Floor(hit.point.x) + 4)];
                                    else
                                        temp0 = GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 5), (int) (Mathf.Floor(hit.point.x) + 4)];
                                    if (temp0.GetComponent<Piece>().isWhite != temp.GetComponent<Piece>().isWhite) {
                                        temp0.GetComponent<Piece>().alive = false;
                                        temp0.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                                        GameManager.GetComponent<GameManager>().dead_pieces.Add(temp0);
                                    }
                                }
                                if (temp.GetComponent<Pawn>() != null && (int) (Mathf.Floor(hit.point.x) + 4) == (int) (transform.position.x + 3.5) &&
                                Mathf.Abs((int) (Mathf.Floor(hit.point.z) + 4) - (int) (transform.position.z + 3.5)) == 2) {
                                    temp.GetComponent<Piece>().en_passant = true;
                                }
                                
                                if (GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)] != null) {
                                    GameObject temp1 = GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)];
                                    temp1.GetComponent<Piece>().alive = false;
                                    temp1.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                                    GameManager.GetComponent<GameManager>().dead_pieces.Add(temp1);
                                }
                                GameManager.GetComponent<GameManager>().game[(int) (Mathf.Floor(hit.point.z) + 4), (int) (Mathf.Floor(hit.point.x) + 4)] = temp;
                                if ((int) (Mathf.Floor(hit.point.x) + 4) == 2 && hasMovedBefore == false && temp.GetComponent<King>() != null) {
                                    GameObject temp2 = GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), 0];
                                    GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), 0] = null;
                                    GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), 3] = temp2;
                                    temp2.GetComponent<Piece>().MovePiece(new Vector3(transform.position.x - 1, 0, transform.position.z));
                                    temp2.GetComponent<Piece>().hasMovedBefore = false;
                                }
                                if ((int) (Mathf.Floor(hit.point.x) + 4) == 6 && hasMovedBefore == false && temp.GetComponent<King>() != null) {
                                    GameObject temp2 = GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), 7];
                                    GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), 7] = null;
                                    GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5f), 5] = temp2;
                                    temp2.GetComponent<Piece>().MovePiece(new Vector3(transform.position.x + 1, 0, transform.position.z));
                                    temp2.GetComponent<Piece>().hasMovedBefore = false;
                                }
                                MovePiece(hit.point);
                                hasMovedBefore = true;
                                GameManager.GetComponent<GameManager>().player1_turn = false;
                                GameManager.GetComponent<GameManager>().player2_turn = false;
                                GameManager.GetComponent<GameManager>().p1_check = false;
                                GameManager.GetComponent<GameManager>().p2_check = false;
                                promote = false;
                                for (int z = 0; z < 8; z++) {
                                    for (int x = 0; x < 8; x++) {
                                        if (GameManager.GetComponent<GameManager>().game[z,x] != null
                                        && GameManager.GetComponent<GameManager>().game[z,x].GetComponent<Pawn>() != null
                                        && (GameManager.GetComponent<GameManager>().game[z,x].GetComponent<Piece>().isWhite == true && z == 7 && GameManager.GetComponent<GameManager>().p_one_AI == false
                                        || GameManager.GetComponent<GameManager>().game[z,x].GetComponent<Piece>().isWhite == false && z == 0 && GameManager.GetComponent<GameManager>().p_two_AI == false)) {
                                            Debug.Log("promotion");
                                            promote = true;
                                        }
                                    }
                                }
                                if (promote == false) {
                                    if (isWhite == true)
                                        GameManager.GetComponent<GameManager>().P2Turn();
                                    else
                                        GameManager.GetComponent<GameManager>().P1Turn();
                                    UnHighlight();
                                }
                            }
                            else
                            {
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
        else
            GetComponent<Collider>().enabled = false;
        if (promote == true && GetComponent<Pawn>() != null) {
            if (Input.GetKeyDown(KeyCode.Alpha1) && isWhite == true) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().knightw, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && isWhite == true) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().rookw, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && isWhite == true) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().bishopw, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && isWhite == true) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().queenw, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && isWhite == false) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().knightb, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && isWhite == false) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().rookb, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && isWhite == false) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().bishopb, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && isWhite == false) {
                GameObject temp = Instantiate(GameManager.GetComponent<GameManager>().queenb, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                temp.GetComponent<Piece>().Summon(isWhite);
                GameManager.GetComponent<GameManager>().game[(int) (transform.position.z + 3.5), (int) (transform.position.x + 3.5)] = temp;
                promote = false;
            }
            if (promote == false) {
                if (isWhite == true)
                    GameManager.GetComponent<GameManager>().P2Turn();
                else
                    GameManager.GetComponent<GameManager>().P1Turn();
                UnHighlight();
                Destroy(gameObject);
            }
        }
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
    public virtual List<Move> getAllMoves(GameObject[] arr) {
        return new List<Move>();
    }
    public int PieceValues(GameObject piece) {
        int points = 0;
        if (piece != null) {
            if (piece.GetComponent<Pawn>() != null) {
                points += 1;
                if (piece.GetComponent<Piece>().isWhite == true) {
                    points += (int) (piece.transform.position.x + 3.5);
                    points += 2 * Mathf.Abs(4 - (int) (piece.transform.position.z + 3.5));  
                }
                else {
                    points += 7 - (int) (piece.transform.position.x + 3.5);
                    points += 2 * Mathf.Abs(4 - (int) (piece.transform.position.z + 3.5));  
                }
            }
            if (piece.GetComponent<Knight>() != null) {
                points += 9;
            }
            if (piece.GetComponent<Rook>() != null) {
                points += 15;
            }
            if (piece.GetComponent<Bishop>() != null) {
                points += 12;
            }
            if (piece.GetComponent<Queen>() != null) {
                points += 20;
            }
            if (piece.GetComponent<King>() != null) {
                points += 100;
            }
        }
        return points;
    }
    public int GetIndex(int z, int x) {
        return z * 8 + x;
    }
}