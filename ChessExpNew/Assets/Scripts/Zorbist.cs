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
    public ulong Hash(GameObject[,] arr) {
        ulong hash = 0;
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                GameObject temp = arr[z,x];
                if (temp != null) {
                    int color = 0;
                    if (temp.GetComponent<Piece>().isWhite == false)
                        color = 1;
                    int pieceType = getType(temp);
                    int square = z * 8 + x;
                    hash ^= values[color, pieceType, square];
                }
            }
        }
        return hash;
    }

    // Update is called once per frame
    public int getType(GameObject piece) {
        if (piece.GetComponent<Pawn>() != null)
            return 1;
        else if (piece.GetComponent<Knight>() != null)
            return 2;
        else if (piece.GetComponent<Rook>() != null)
            return 3;
        else if (piece.GetComponent<Bishop>() != null)
            return 4;
        else if (piece.GetComponent<Queen>() != null)
            return 5;
        else
            return 6;
    }
    //public void UpdateHash(ref ulong hash, Move move, GameObject[,] arr) {

    //}
}
