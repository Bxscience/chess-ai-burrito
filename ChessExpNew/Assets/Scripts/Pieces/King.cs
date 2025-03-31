using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Move> getAllMoves(GameObject[,] arr) { // all movements // similar to queen code but lessen loop
        List<Move> moves = new List<Move>();
        for (int a = 1; a < 2; a++) {
            if (transform.position.z + 3.5 + a <= 7) {
                if (arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f)])));
                    break;                
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (transform.position.z + 3.5 - a >= 0) {
                if (arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f)])));
                    break;                
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (transform.position.x + 3.5 + a <= 7) {
                if (arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f + a)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f + a),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f + a)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f + a),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f + a)])));
                    break;                
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (transform.position.x + 3.5 - a >= 0) {
                if (arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f - a)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f - a),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f - a)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f - a),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f - a)])));
                    break;                
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (transform.position.z + 3.5 + a <= 7 && transform.position.x + 3.5 + a <= 7) {
                if (arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f + a)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f + a),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f + a)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f + a),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f + a)])));
                    break;                
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (transform.position.z + 3.5 - a >= 0 && transform.position.x + 3.5 - a >= 0) {
                if (arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f - a)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f - a),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f - a)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f - a),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f - a)])));
                    break;                
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (transform.position.z + 3.5 - a >= 0 && transform.position.x + 3.5 + a <= 7) {
                if (arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f + a)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f + a),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f + a)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f + a),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f - a), (int) (transform.position.x + 3.5f + a)])));
                    break;                
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (transform.position.z + 3.5 + a <= 7 && transform.position.x + 3.5 - a >= 0) {
                if (arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f - a)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f - a),
                    200,
                    0));
                else {
                    if (arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f - a)].GetComponent<Piece>().isWhite != isWhite)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f - a),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 3.5f + a), (int) (transform.position.x + 3.5f - a)])));
                    break;                
                }
            }
            else
                break;
        }
        
        if (hasMovedBefore == false && arr[(int) (transform.position.z + 3.5), 0] != null &&
        GameManager.GetComponent<GameManager>().player1_turn == true && isWhite == true 
        || GameManager.GetComponent<GameManager>().player2_turn == true && isWhite == false) {
            bool bad = false;
            for (int a = 0; a <= 2; a++) {
                if (arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f - a)] != null ||
                danger(isWhite, arr, (int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f - a)) == true) {
                    bad = true;
                    break;
                }
            }
            if (bad == false) {
                Move temp = new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f), 2, 200, 0);
                temp.castleLeft((int) (transform.position.z + 3.5f));
                moves.Add(temp);
            }
        }
        if (hasMovedBefore == false && arr[(int) (transform.position.z + 3.5), 7] != null &&
        GameManager.GetComponent<GameManager>().player1_turn == true && isWhite == true 
        || GameManager.GetComponent<GameManager>().player2_turn == true && isWhite == false) {
            bool bad = false;
            for (int a = 0; a <= 2; a++) {
                if (arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f + a)] != null ||
                danger(isWhite, arr, (int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f + a)) == true) {
                    bad = true;
                    break;
                }
            }
            if (bad == false) {
                Move temp = new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 3.5f), 6, 200, 0);
                temp.castleRight((int) (transform.position.z + 3.5f));
                moves.Add(temp);
            }
        }
        
        if (GameManager.GetComponent<GameManager>().player1_turn == true && isWhite == true //check if moves put king in danger
        || GameManager.GetComponent<GameManager>().player2_turn == true && isWhite == false) {
            GameObject[,] arr2 = new GameObject[8,8];
            for (int a = 0; a < moves.Count; a++) {
                for (int z = 0; z < 8; z++)
                    for (int x = 0; x < 8; x++) {
                        arr2[z,x] = arr[z,x];
                    }
                arr2[moves[a].endz, moves[a].endx] = arr2[moves[a].startz, moves[a].startx];
                arr2[moves[a].startz, moves[a].startx] = null;
                if (danger(isWhite, arr2, moves[a].endz, moves[a].endx) == true) {
                    moves.RemoveAt(a);
                    a--;
                }
            }
        }
        
        return moves;
    }
    
    public bool danger(bool white, GameObject[,] arr) {
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z, x] != null) {
                    if (arr[z, x].GetComponent<Piece>().isWhite != white) {
                        List<Move> temp = arr[z, x].GetComponent<Piece>().getAllMoves(arr); // checks all opposing pieces to see if they affect the king
                        for (int a = 0; a < temp.Count; a++)
                            if (temp[a].endz == transform.position.z + 3.5 && temp[a].endx == transform.position.x + 3.5) {
                                Resources.UnloadUnusedAssets();
                                return true;
                            }
                    }
                }
            }
        }
        Resources.UnloadUnusedAssets();
        return false;
    }
    public bool danger(bool white, GameObject[,] arr, int ez, int ex) {
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z, x] != null) {
                    if (arr[z, x].GetComponent<Piece>().isWhite != white) {
                        List<Move> temp = arr[z, x].GetComponent<Piece>().getAllMoves(arr);
                        for (int a = 0; a < temp.Count; a++)
                            if (temp[a].endz == ez && temp[a].endx == ex) {
                                Resources.UnloadUnusedAssets();
                                return true;
                            }
                    }
                }
            }
        }
        Resources.UnloadUnusedAssets();
        return false;
    }
    public bool danger(bool white, GameObject[] arr) {
        for (int x = 0; x < 64; x++) {
            if (arr[x] != null) {
                if (arr[x].GetComponent<Piece>().isWhite != white) {
                    List<Move> temp = arr[x].GetComponent<Piece>().getAllMoves(arr); // checks all opposing pieces to see if they affect the king
                    for (int a = 0; a < temp.Count; a++)
                        if (temp[a].endz == transform.position.z + 3.5 && temp[a].endx == transform.position.x + 3.5) {
                            Resources.UnloadUnusedAssets();
                            return true;
                        }
                }
            }
        }
        Resources.UnloadUnusedAssets();
        return false;
    }
    public bool danger(bool white, GameObject[] arr, int ez, int ex) {
        for (int x = 0; x < 64; x++) {
            if (arr[x] != null) {
                if (arr[x].GetComponent<Piece>().isWhite != white) {
                    List<Move> temp = arr[x].GetComponent<Piece>().getAllMoves(arr);
                    for (int a = 0; a < temp.Count; a++)
                        if (temp[a].endz == ez && temp[a].endx == ex) {
                            Resources.UnloadUnusedAssets();
                            return true;
                        }
                }
            }
        }
        Resources.UnloadUnusedAssets();
        return false;
    }
    public bool checkmate(bool white) { // if the king cannot escape, checkmate, else check
        List<Move> temp = getAllMoves(GameManager.GetComponent<GameManager>().game);
        for (int a = 0; a < temp.Count; a++) {
            GameObject[,] arr = new GameObject[8,8];
            for (int z = 0; z < 8; z++)
                for (int x = 0; x < 8; x++) {
                    arr[z,x] = GameManager.GetComponent<GameManager>().game[z,x];
                }
            arr[temp[a].endz, temp[a].endx] = arr[temp[a].startz, temp[a].startx];
            arr[temp[a].startz, temp[a].startx] = null;
            if (danger(white, arr, temp[a].endz, temp[a].endx) == false) {
                Debug.Log("check");
                Resources.UnloadUnusedAssets();
                return false;
            }
            Resources.UnloadUnusedAssets();
        }
        Debug.Log("checkmate");
        return true;
    }
    public bool stalemate(bool white) { // if the king is not in check but cannot move safely, and there are no available moves, stalemate
        List<Move> temp = getAllMoves(GameManager.GetComponent<GameManager>().game);
        for (int a = 0; a < temp.Count; a++) {
            GameObject[,] arr = new GameObject[8,8];
            for (int z = 0; z < 8; z++)
                for (int x = 0; x < 8; x++) {
                    arr[z,x] = GameManager.GetComponent<GameManager>().game[z,x];
                }
            arr[temp[a].endz, temp[a].endx] = arr[temp[a].startz, temp[a].startx];
            arr[temp[a].startz, temp[a].startx] = null;
            if (danger(white, arr, temp[a].endz, temp[a].endx) == false) {
                Debug.Log("no stalemate");
                Resources.UnloadUnusedAssets();
                return false;
            }
            Resources.UnloadUnusedAssets();
        }
        List<Move> temp0 = new List<Move>();
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (GameManager.GetComponent<GameManager>().game[z,x] != null)
                    temp0.AddRange(GameManager.GetComponent<GameManager>().game[z,x].GetComponent<Piece>().getAllMoves(GameManager.GetComponent<GameManager>().game));
            }
        }
        if (temp0.Count == 0) {
            Debug.Log("stalemate");
            return true;
        }
        return false;
    }
}