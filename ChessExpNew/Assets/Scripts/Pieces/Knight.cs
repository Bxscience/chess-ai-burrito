using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Move> getAllMoves(GameObject[,] arr) { // get all L shapes
        List<Move> moves = new List<Move>();
        if (transform.position.z + 3.5f < 7 && transform.position.x + 3.5 > 1)
            if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 1.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 1.5f)));
            else
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 1.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 1.5f)));
        if (transform.position.z + 3.5f < 7 && transform.position.x + 3.5 < 6)
            if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 5.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 5.5f)));
            else
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 5.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 5.5f)));
        if (transform.position.z + 3.5f > 0 && transform.position.x + 3.5 > 1)
            if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 1.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 1.5f)));
            else
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 1.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 1.5f)));
        if (transform.position.z + 3.5f > 0 && transform.position.x + 3.5 < 6)
            if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 5.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 5.5f)));
            else
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 5.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 5.5f)));
        if (transform.position.z + 3.5f < 6 && transform.position.x + 3.5 > 0)
            if (arr[(int) (transform.position.z + 5.5f), (int) (transform.position.x + 2.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 5.5f), (int) (transform.position.x + 2.5f)));
            else
                if (arr[(int) (transform.position.z + 5.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 5.5f), (int) (transform.position.x + 2.5f)));
        if (transform.position.z + 3.5f < 6 && transform.position.x + 3.5 < 7)
            if (arr[(int) (transform.position.z + 5.5f), (int) (transform.position.x + 4.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 5.5f), (int) (transform.position.x + 4.5f)));
            else
                if (arr[(int) (transform.position.z + 5.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 5.5f), (int) (transform.position.x + 4.5f)));
        if (transform.position.z + 3.5f > 1 && transform.position.x + 3.5 > 0)
            if (arr[(int) (transform.position.z + 1.5f), (int) (transform.position.x + 2.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 1.5f), (int) (transform.position.x + 2.5f)));
            else
                if (arr[(int) (transform.position.z + 1.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 1.5f), (int) (transform.position.x + 2.5f)));
        if (transform.position.z + 3.5f > 1 && transform.position.x + 3.5 < 7)
            if (arr[(int) (transform.position.z + 1.5f), (int) (transform.position.x + 4.5f)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 1.5f), (int) (transform.position.x + 4.5f)));
            else
                if (arr[(int) (transform.position.z + 1.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite != isWhite)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 1.5f), (int) (transform.position.x + 4.5f)));
        return moves;
    }
}