using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour // Main class that handles the game
{
    public GameObject pawnw;
    public GameObject rookw;
    public GameObject knightw;
    public GameObject bishopw;
    public GameObject queenw;
    public GameObject kingw;

    public GameObject pawnb;
    public GameObject rookb;
    public GameObject knightb;
    public GameObject bishopb;
    public GameObject queenb;
    public GameObject kingb;
    //all the pieces
    public GameObject block; // tiles that gets highlighted yellow
    public List<GameObject> dead_pieces = new List<GameObject>(); // all dead pieces


    public int turn;
    public GameObject[,] game = new GameObject[8, 8]; // main game, everything is done through this
    public GameObject[,] blocks = new GameObject[8, 8];
    public bool end;
    public bool player1_turn;
    public bool player2_turn;
    public bool p_one_AI;
    public bool p_two_AI;
    public bool p1_check;
    public bool p2_check;
    public int rule = 0;
    public AIScript AI1;
    public AIScript AI2;

    // Start is called before the first frame update
    void Start()
    {
        InitializeBoard();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) { // to test that everything is in the correct place (code and visuals match)
            for (int z = 0; z < 8; z++)
                for (int x = 0; x < 8; x++)
                    if (game[z, x] != null)
                        Debug.Log(z + ", " + x + ": " + game[z, x].name);
                    else
                        Debug.Log(z + ", " + x + ": null");
        }
    }

    void InitializeBoard() {
        for (int z = 0; z < 8; z++)
            for (int x = 0; x < 8; x++) {
                GameObject temp = Instantiate(block, new Vector3(x - 3.5f, -.5f, z - 3.5f), Quaternion.identity);
                if ((z+x) % 2 == 0)
                    temp.GetComponent<Renderer>().material.color = Color.white;
                else
                    temp.GetComponent<Renderer>().material.color = Color.black;
                blocks[z,x] = temp;
            } // instantiate blocks
        game[0, 0] = Instantiate(rookw, new Vector3(-3.5f, 0, -3.5f), Quaternion.identity);
        game[0, 7] = Instantiate(rookw, new Vector3(3.5f, 0, -3.5f), Quaternion.identity);
        game[0, 1] = Instantiate(knightw, new Vector3(-2.5f, 0, -3.5f), Quaternion.identity);
        game[0, 6] = Instantiate(knightw, new Vector3(2.5f, 0, -3.5f), Quaternion.identity);
        game[0, 2] = Instantiate(bishopw, new Vector3(-1.5f, 0, -3.5f), Quaternion.identity);
        game[0, 5] = Instantiate(bishopw, new Vector3(1.5f, 0, -3.5f), Quaternion.identity);
        game[0, 3] = Instantiate(queenw, new Vector3(-.5f, 0, -3.5f), Quaternion.identity);
        game[0, 4] = Instantiate(kingw, new Vector3(.5f, 0, -3.5f), Quaternion.identity);
        for (int x = 0; x < 8; x++) {
            game[1, x] = Instantiate(pawnw, new Vector3(-3.5f + x, 0, -2.5f), Quaternion.identity);
        }

        game[7, 0] = Instantiate(rookb, new Vector3(-3.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 7] = Instantiate(rookb, new Vector3(3.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 1] = Instantiate(knightb, new Vector3(-2.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 6] = Instantiate(knightb, new Vector3(2.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 2] = Instantiate(bishopb, new Vector3(-1.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 5] = Instantiate(bishopb, new Vector3(1.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 3] = Instantiate(queenb, new Vector3(-.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 4] = Instantiate(kingb, new Vector3(.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        for (int x = 0; x < 8; x++) {
            game[6, x] = Instantiate(pawnb, new Vector3(-3.5f + x, 0, 2.5f), Quaternion.Euler(0, 180, 0));
        }
        //instantiate pieces
        end = false;
        turn = 0;
        p_one_AI = false;
        p_two_AI = true;
        if (p_one_AI) {
            AI1 = new AIScript();
            AI1.zorbist.getValues();
        }
        if (p_two_AI) {
            AI2 = new AIScript();
            AI2.zorbist.getValues();
        }
        p1_check = false;
        p2_check = false;
        player1_turn = false;
        player2_turn = false;
        P1Turn();
    }
    public void P1Turn() {
        if (end == false) {
            turn += 1;
            rule += 1;
            bool others = false;
            for (int z = 0; z < 8; z++) {
                for (int x = 0; x < 8; x++) {
                    if (game[z,x] != null && game[z,x].GetComponent<King>() != true) {
                        others = true;
                        break;
                    }
                }
            }
            if (others == false) {
                end = true;
                return;
            }
            if (rule == 50) {
                end = true;
                return;
            }
            for (int z = 0; z < 8; z++)
                for (int x = 0; x < 8; x++)
                    if (game[z,x] != null && game[z,x].GetComponent<Piece>().en_passant == true && game[z,x].GetComponent<Piece>().isWhite == true)
                        game[z,x].GetComponent<Piece>().en_passant = false;
            if (turn != 1) { // performs checks of danger before allowing the turn
                GameObject king = GameObject.Find("KingLight(Clone)");
                if (king.GetComponent<King>().alive == false) {
                    end = true;
                    return;
                }
                if (king.GetComponent<King>().danger(true, game)) {
                    if (king.GetComponent<King>().checkmate(true)) {
                        end = true;
                        return;
                    }
                    else
                        p1_check = true;
                }
                if (king.GetComponent<King>().stalemate(true)) {
                    end = true;
                    return;
                }
                
                
            }
            if (p_one_AI == false) {
                player1_turn = true;
            }
            else {
                Move best = AI1.BestMove(game, true);
                GameObject temp = game[best.startz, best.startx];
                game[best.startz, best.startx] = null;
                if (game[best.endz, best.endx] != null) {
                    rule = 0;
                    GameObject temp1 = game[best.endz, best.endx];
                    temp1.GetComponent<Piece>().alive = false;
                    temp1.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                }
                if (temp.GetComponent<Pawn>() != null)
                    rule = 0;
                if (temp.GetComponent<Pawn>() != null && game[best.endz, best.endx] == null &&
                best.endz != best.startz && best.endx != best.startx) {
                    rule = 0;
                    GameObject temp1 = game[best.startz, best.endx];
                    temp1.GetComponent<Piece>().alive = false;
                    temp1.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                    dead_pieces.Add(temp1);
                }
                game[best.endz, best.endx] = temp;
                game[best.endz, best.endx].GetComponent<Piece>().hasMovedBefore = true;
                game[best.endz, best.endx].GetComponent<Piece>().MovePiece(new Vector3 (best.endx - 3.5f, 0, best.endz - 3.5f));
                if (best.promotion != 0) {
                    if (best.promotion == 1) {
                        GameObject temp0 = Instantiate(knightb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(true);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                    if (best.promotion == 2) {
                        GameObject temp0 = Instantiate(rookb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(true);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                    if (best.promotion == 3) {
                        GameObject temp0 = Instantiate(bishopb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(true);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                    if (best.promotion == 4) {
                        GameObject temp0 = Instantiate(queenb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(true);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                }
                player1_turn = false;
                player2_turn = false;
                p1_check = false;
                p2_check = false;
                P2Turn();
            }
        }
    }
    public void P2Turn() {
        if (end == false) {
            for (int z = 0; z < 8; z++)
                for (int x = 0; x < 8; x++)
                    if (game[z,x] != null && game[z,x].GetComponent<Piece>().en_passant == true && game[z,x].GetComponent<Piece>().isWhite == false)
                        game[z,x].GetComponent<Piece>().en_passant = false;
            GameObject king = GameObject.Find("KingDark(Clone)");
            if (king.GetComponent<King>().alive == false) {
                end = true;
                return;
            }
            if (king.GetComponent<King>().danger(false, game)) {
                if (king.GetComponent<King>().checkmate(false)) {
                    end = true;
                    return;
                }
                else
                    p2_check = true;
            }
            king = GameObject.Find("KingLight(Clone)");
            if (king.GetComponent<King>().stalemate(false)) {
                end = true;
                return;
            }
            if (p_two_AI == false) {
                player2_turn = true;
            }
            else {
                Move best = AI2.BestMove(game, false);
                GameObject temp = game[best.startz, best.startx];
                game[best.startz, best.startx] = null;
                if (game[best.endz, best.endx] != null) {
                    rule = 0;
                    GameObject temp1 = game[best.endz, best.endx];
                    temp1.GetComponent<Piece>().alive = false;
                    temp1.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                }
                if (temp.GetComponent<Pawn>() != null)
                    rule = 0;
                if (temp.GetComponent<Pawn>() != null && game[best.endz, best.endx] == null &&
                best.endz != best.startz && best.endx != best.startx) {
                    rule = 0;
                    GameObject temp1 = game[best.startz, best.endx];
                    temp1.GetComponent<Piece>().alive = false;
                    temp1.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                    dead_pieces.Add(temp1);
                }
                game[best.endz, best.endx] = temp;
                game[best.endz, best.endx].GetComponent<Piece>().hasMovedBefore = true;
                game[best.endz, best.endx].GetComponent<Piece>().MovePiece(new Vector3 (best.endx - 3.5f, 0, best.endz - 3.5f));
                if (best.promotion != 0) {
                    if (best.promotion == 1) {
                        GameObject temp0 = Instantiate(knightb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(false);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                    if (best.promotion == 2) {
                        GameObject temp0 = Instantiate(rookb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(false);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                    if (best.promotion == 3) {
                        GameObject temp0 = Instantiate(bishopb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(false);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                    if (best.promotion == 4) {
                        GameObject temp0 = Instantiate(queenb, new Vector3(game[best.endz, best.endx].transform.position.x, 0, game[best.endz, best.endx].transform.position.z), Quaternion.identity);
                        temp0.GetComponent<Piece>().Summon(false);
                        Destroy(game[best.endz, best.endx]);
                        game[best.endz, best.endx] = temp0;
                    }
                }
                player1_turn = false;
                player2_turn = false;
                p1_check = false;
                p2_check = false;
                P1Turn();
            }
        }
    }
}
