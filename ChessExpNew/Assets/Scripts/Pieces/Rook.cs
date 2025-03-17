using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Move> getAllMoves(GameObject[,] arr) { // uses a loop to move up and down and side to side
        List<Move> moves = new List<Move>();
        for (int a = 1; a < 8; a++) {
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
        for (int a = 1; a < 8; a++) {
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
        for (int a = 1; a < 8; a++) {
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
        for (int a = 1; a < 8; a++) {
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
        return moves;
    }
}