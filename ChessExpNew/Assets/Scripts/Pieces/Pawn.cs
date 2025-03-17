using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Move> getAllMoves(GameObject[,] arr) {
        // one of the many classes that extend piece
        // convert real unity movements to movements along the array
        // positions start at transform.position.z or x + 3.5 because on the main board, 0,0 on the array is -3.5, -3.5 in reality
        List<Move> moves = new List<Move>();
        if (isWhite) {
            if (transform.position.z + 3.5f < 7)
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5),
                    200,
                    0));
            if (hasMovedBefore == false && arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5)] == null
            && arr[(int) (transform.position.z + 5.5f), (int) (transform.position.x + 3.5)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 5.5f), (int) (transform.position.x + 3.5),
                200,
                0));
            if (transform.position.x + 3.5f < 7 && transform.position.z + 3.5f < 7) {
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)] != null)
                    if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite == false)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)])));
            if (transform.position.x + 3.5f > 0 && transform.position.z + 3.5f < 7) 
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)] != null)
                    if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite == false)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)])));
            }
        }
        else {
            if (transform.position.z + 3.5f > 0) {
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5),
                    200,
                    0));
            }
            if (hasMovedBefore == false && arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5)] == null
            && arr[(int) (transform.position.z + 1.5f), (int) (transform.position.x + 3.5)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 1.5f), (int) (transform.position.x + 3.5),
                200,
                0));
            if (transform.position.x + 3.5f < 7 && transform.position.z + 3.5f > 0)
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)] != null)
                    if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite == true)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)])));
            if (transform.position.x + 3.5f > 0 && transform.position.z + 3.5f > 0)    
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)] != null)
                    if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite == true)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)])));
        }
        return moves;
    }
}