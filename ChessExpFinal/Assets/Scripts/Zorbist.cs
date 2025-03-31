using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zorbist
{
    // Important Note: I used ChatGPT to explain and generate code used for zorbist hashing, 
    // a technique that I was unfamiliar with
    public ulong[,,] values = new ulong[2, 6, 64];
    public ulong turn;
    public System.Random rng = new System.Random(123456);
    // Start is called before the first frame update
    public void getValues() {
        for (int color = 0; color < 2; color++) {
            for (int piece = 0; piece < 6; piece++) {
                for (int square = 0; square < 64; square++) {
                    values[color, piece, square] = ((ulong)rng.Next() << 32) | (ulong)rng.Next();
                }
            }
        }
        turn = ((ulong)rng.Next() << 32) | (ulong)rng.Next();
    }
    public ulong Hash(int[] arr, bool maximizingPlayer) {
        ulong hash = 0;
        for (int x = 0; x < 64; x++) {
            int temp = arr[x];
            if (temp != 0) {
                int color = 0;
                if (temp < 0)
                    color = 1;
                int pieceType = Mathf.Abs(temp) % 100 - 1;
                //Debug.Log(values[color, pieceType, x]);
                hash ^= values[color, pieceType, x];
            }
        }
        if (maximizingPlayer == false)
            hash ^= turn;
        return hash;
    }

    // Update is called once per frame
    public int getType(GameObject piece) {
        if (piece.GetComponent<Pawn>() != null)
            return 0;
        else if (piece.GetComponent<Knight>() != null)
            return 1;
        else if (piece.GetComponent<Rook>() != null)
            return 2;
        else if (piece.GetComponent<Bishop>() != null)
            return 3;
        else if (piece.GetComponent<Queen>() != null)
            return 4;
        else
            return 5;
    }
    
    public ulong UpdateHash(ref ulong hash, Move move, GameObject[] arr) {
        int fromSquare = move.startz * 8 + move.startx;
        int toSquare = move.endz * 8 + move.endx;
        int color = arr[move.startz * 8 + move.startx].GetComponent<Piece>().isWhite ? 0 : 1;
        int pieceType = (int) getType(arr[move.startz * 8 + move.startx]);

        // Remove piece from old position
        hash ^= values[color, pieceType, fromSquare];

        // Remove captured piece if any
        if (arr[move.endz * 8 + move.endx] != null) {
            int capturedColor = arr[move.endz * 8 + move.endx].GetComponent<Piece>().isWhite ? 0 : 1;
            int capturedPieceType = (int) getType(arr[move.endz * 8 + move.endx]);
            hash ^= values[capturedColor, capturedPieceType, toSquare];
        }

        // Add piece to new position
        hash ^= values[color, pieceType, toSquare];

        // Toggle turn hash
        hash ^= turn;
        return hash;
    }
    public ulong UndoHash(ref ulong hash, Move move, GameObject[] arr, GameObject old) {
        int fromSquare = move.startz * 8 + move.startx;
        int toSquare = move.endz * 8 + move.endx;
        int color = arr[move.endz * 8 + move.endx].GetComponent<Piece>().isWhite ? 0 : 1;
        int pieceType = (int) getType(arr[move.endz * 8 + move.endx]);

        // Remove piece from old position
        hash ^= values[color, pieceType, toSquare];

        // Add piece to new position
        hash ^= values[color, pieceType, fromSquare];

        // Add captured piece if any
        if (old != null) {
            int capturedColor = old.GetComponent<Piece>().isWhite ? 0 : 1;
            int capturedPieceType = (int) getType(old);
            hash ^= values[capturedColor, capturedPieceType, toSquare];
        }

        // Toggle turn hash
        hash ^= turn;
        return hash;
    }
    
}
