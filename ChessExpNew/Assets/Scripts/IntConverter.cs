using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntConverter
{
    // Start is called before the first frame update
    public int[] convert(GameObject[,] arr) {
        int[] ints = new int[64];
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z,x] != null) {
                    if (arr[z,x].GetComponent<Pawn>() != null && arr[z,x].GetComponent<Piece>().isWhite == true)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = 1;
                        else
                            ints[z*8+x] = 101;
                    else if (arr[z,x].GetComponent<Knight>() != null && arr[z,x].GetComponent<Piece>().isWhite == true)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = 2;
                        else
                            ints[z*8+x] = 102;
                    else if (arr[z,x].GetComponent<Rook>() != null && arr[z,x].GetComponent<Piece>().isWhite == true)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = 3;
                        else
                            ints[z*8+x] = 103;
                    else if (arr[z,x].GetComponent<Bishop>() != null && arr[z,x].GetComponent<Piece>().isWhite == true)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = 4;
                        else
                            ints[z*8+x] = 104;
                    else if (arr[z,x].GetComponent<Queen>() != null && arr[z,x].GetComponent<Piece>().isWhite == true)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = 5;
                        else
                            ints[z*8+x] = 105; 
                    else if (arr[z,x].GetComponent<King>() != null && arr[z,x].GetComponent<Piece>().isWhite == true)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = 6;
                        else
                            ints[z*8+x] = 106;
                    else if (arr[z,x].GetComponent<Pawn>() != null && arr[z,x].GetComponent<Piece>().isWhite == false)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = -1;
                        else
                            ints[z*8+x] = -101;
                    else if (arr[z,x].GetComponent<Knight>() != null && arr[z,x].GetComponent<Piece>().isWhite == false)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = -2;
                        else
                            ints[z*8+x] = -102;
                    else if (arr[z,x].GetComponent<Rook>() != null && arr[z,x].GetComponent<Piece>().isWhite == false)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = -3;
                        else
                            ints[z*8+x] = -103;
                    else if (arr[z,x].GetComponent<Bishop>() != null && arr[z,x].GetComponent<Piece>().isWhite == false)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = -4;
                        else
                            ints[z*8+x] = -104;
                    else if (arr[z,x].GetComponent<Queen>() != null && arr[z,x].GetComponent<Piece>().isWhite == false)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = -5;
                        else
                            ints[z*8+x] = -105;
                    else if (arr[z,x].GetComponent<King>() != null && arr[z,x].GetComponent<Piece>().isWhite == false)
                        if (arr[z,x].GetComponent<Piece>().hasMovedBefore == true)
                            ints[z*8+x] = -6;
                        else
                            ints[z*8+x] = -106;
                }
            }
        }
        return ints;
    }
    public List<Move> getMoves(int[] arr, bool color) {
        List<Move> all_moves = new List<Move>();
        for (int x = 0; x < 64; x++) {
            if (Mathf.Abs(arr[x]) % 100 == 1 && (color == true && arr[x] > 0 || color == false && arr[x] < 0))
                all_moves.AddRange(Pawn(arr, color, x));
            else if (Mathf.Abs(arr[x]) % 100 == 2 && (color == true && arr[x] > 0 || color == false && arr[x] < 0))
                all_moves.AddRange(Knight(arr, color, x));
            else if (Mathf.Abs(arr[x]) % 100 == 3 && (color == true && arr[x] > 0 || color == false && arr[x] < 0))
                all_moves.AddRange(Rook(arr, color, x));
            else if (Mathf.Abs(arr[x]) % 100 == 4 && (color == true && arr[x] > 0 || color == false && arr[x] < 0))
                all_moves.AddRange(Bishop(arr, color, x));
            else if (Mathf.Abs(arr[x]) % 100 == 5 && (color == true && arr[x] > 0 || color == false && arr[x] < 0))
                all_moves.AddRange(Queen(arr, color, x));
            else if (Mathf.Abs(arr[x]) % 100 == 6 && (color == true && arr[x] > 0 || color == false && arr[x] < 0))
                all_moves.AddRange(King(arr, color, x));
        }
        
        return all_moves;
    }
    private List<Move> Pawn(int[] arr, bool color, int start) {
        List<Move> moves = new List<Move>();
        int z = start / 8;
        int x = start % 8;
        if (color) {
            if (z < 7 && arr[(z+1)*8 + x] == 0)
                if (z+1 != 7)
                    moves.Add(new Move(z, x, z + 1, x, 200, 0));
                else {
                    moves.Add(new Move(z, x, z + 1, x, 200, 0, 1));
                    moves.Add(new Move(z, x, z + 1, x, 200, 0, 2));
                    moves.Add(new Move(z, x, z + 1, x, 200, 0, 3));
                    moves.Add(new Move(z, x, z + 1, x, 200, 0, 4));
                }
            if (arr[start] > 100 && arr[start] < 200 && arr[(z+1)*8 + x] == 0 && arr[(z+2)*8 + x] == 0)
                moves.Add(new Move(z, x, z + 2, x, 200, 0, true));
            if (z < 7 && x < 7 && arr[(z+1)*8 + (x+1)] < 0)
                if (z+1 != 7)
                    moves.Add(new Move(z, x, z + 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x+1)])));
                else {
                    moves.Add(new Move(z, x, z + 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x+1)]), 1));
                    moves.Add(new Move(z, x, z + 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x+1)]), 2));
                    moves.Add(new Move(z, x, z + 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x+1)]), 3));
                    moves.Add(new Move(z, x, z + 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x+1)]), 4));
                }
            if (z < 7 && x < 7 && arr[(z)*8 + (x+1)] < -200)
                moves.Add(new Move(z, x, z + 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x+1)])));
            if (z < 7 && x > 0 && arr[(z+1)*8 + (x-1)] < 0)
                if (z+1 != 7)
                    moves.Add(new Move(z, x, z + 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x-1)])));
                else {
                    moves.Add(new Move(z, x, z + 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x-1)]), 1));
                    moves.Add(new Move(z, x, z + 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x-1)]), 2));
                    moves.Add(new Move(z, x, z + 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x-1)]), 3));
                    moves.Add(new Move(z, x, z + 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x-1)]), 4));
                }
            if (z < 7 && x > 0 && arr[(z)*8 + (x-1)] < -200)
                moves.Add(new Move(z, x, z + 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x-1)])));
        }
        else {
            if (z > 0 && arr[(z-1)*8 + x] == 0)
                if (z-1 != 0)
                    moves.Add(new Move(z, x, z - 1, x, 200, 0));
                else {
                    moves.Add(new Move(z, x, z - 1, x, 200, 0, 1));
                    moves.Add(new Move(z, x, z - 1, x, 200, 0, 2));
                    moves.Add(new Move(z, x, z - 1, x, 200, 0, 3));
                    moves.Add(new Move(z, x, z - 1, x, 200, 0, 4));
                }
            if (arr[start] < -100 && arr[start] > -200 && arr[(z-1)*8 + x] == 0 && arr[(z-2)*8 + x] == 0)
                moves.Add(new Move(z, x, z - 2, x, 200, 0, true));
            if (z > 0 && x < 7 && arr[(z-1)*8 + (x+1)] > 0)
                if (z-1 != 0)
                    moves.Add(new Move(z, x, z - 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x+1)])));
                else {
                    moves.Add(new Move(z, x, z - 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x+1)]), 1));
                    moves.Add(new Move(z, x, z - 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x+1)]), 2));
                    moves.Add(new Move(z, x, z - 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x+1)]), 3));
                    moves.Add(new Move(z, x, z - 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x+1)]), 4));
                }
            if (z > 0 && x < 7 && arr[(z)*8 + (x+1)] > 200)
                moves.Add(new Move(z, x, z - 1, x + 1, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x+1)])));
            if (z > 0 && x > 0 && arr[(z-1)*8 + (x-1)] > 0)
                if (z-1 != 0)
                    moves.Add(new Move(z, x, z - 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x-1)])));
                else {
                    moves.Add(new Move(z, x, z - 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x-1)]), 1));
                    moves.Add(new Move(z, x, z - 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x-1)]), 2));
                    moves.Add(new Move(z, x, z - 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x-1)]), 3));
                    moves.Add(new Move(z, x, z - 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x-1)]), 4));
                }
            if (z > 0 && x > 0 && arr[(z)*8 + (x-1)] > 200)
                moves.Add(new Move(z, x, z - 1, x - 1, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x-1)])));
        }
        return moves;
    }
    private List<Move> Knight(int[] arr, bool color, int start) {
        List<Move> moves = new List<Move>();
        int z = start / 8;
        int x = start % 8;
        if (z < 7 && x > 1) {
            if (arr[(z+1)*8 + x-2] == 0)
                moves.Add(new Move(z, x, z + 1, x - 2, 200, 0));
            else
                if (arr[start] > 0 && arr[(z+1)*8 + x-2] < 0 || arr[start] < 0 && arr[(z+1)*8 + x-2] > 0)
                    moves.Add(new Move(z, x, z + 1, x - 2, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x-2)])));
        }
        if (z < 7 && x < 6) {
            if (arr[(z+1)*8 + x+2] == 0)
                moves.Add(new Move(z, x, z + 1, x + 2, 200, 0));
            else
                if (arr[start] > 0 && arr[(z+1)*8 + x+2] < 0 || arr[start] < 0 && arr[(z+1)*8 + x+2] > 0)
                    moves.Add(new Move(z, x, z + 1, x + 2, PieceValues(arr[start]), PieceValues(arr[(z+1)*8 + (x+2)])));
        }
        if (z > 0 && x > 1) {
            if (arr[(z-1)*8 + x-2] == 0)
                moves.Add(new Move(z, x, z - 1, x - 2, 200, 0));
            else
                if (arr[start] > 0 && arr[(z-1)*8 + x-2] < 0 || arr[start] < 0 && arr[(z-1)*8 + x-2] > 0)
                    moves.Add(new Move(z, x, z - 1, x - 2, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x-2)])));
        }
        if (z > 0 && x < 6) {
            if (arr[(z-1)*8 + x+2] == 0)
                moves.Add(new Move(z, x, z - 1, x + 2, 200, 0));
            else
                if (arr[start] > 0 && arr[(z-1)*8 + x+2] < 0 || arr[start] < 0 && arr[(z-1)*8 + x+2] > 0)
                    moves.Add(new Move(z, x, z - 1, x + 2, PieceValues(arr[start]), PieceValues(arr[(z-1)*8 + (x+2)])));
        }
        if (z < 6 && x > 0) {
            if (arr[(z+2)*8 + x-1] == 0)
                moves.Add(new Move(z, x, z + 2, x - 1, 200, 0));
            else
                if (arr[start] > 0 && arr[(z+2)*8 + x-1] < 0 || arr[start] < 0 && arr[(z+2)*8 + x-1] > 0)
                    moves.Add(new Move(z, x, z + 2, x - 1, PieceValues(arr[start]), PieceValues(arr[(z+2)*8 + (x-1)])));
        }
        if (z < 6 && x < 7) {
            if (arr[(z+2)*8 + x+1] == 0)
                moves.Add(new Move(z, x, z + 2, x + 1, 200, 0));
            else
                if (arr[start] > 0 && arr[(z+2)*8 + x+1] < 0 || arr[start] < 0 && arr[(z+2)*8 + x+1] > 0)
                    moves.Add(new Move(z, x, z + 2, x + 1, PieceValues(arr[start]), PieceValues(arr[(z+2)*8 + (x+1)])));
        }
        if (z > 1 && x > 0) {
            if (arr[(z-2)*8 + x-1] == 0)
                moves.Add(new Move(z, x, z - 2, x - 1, 200, 0));
            else
                if (arr[start] > 0 && arr[(z-2)*8 + x-1] < 0 || arr[start] < 0 && arr[(z-2)*8 + x-1] > 0)
                    moves.Add(new Move(z, x, z - 2, x - 1, PieceValues(arr[start]), PieceValues(arr[(z-2)*8 + (x-1)])));
        }
        if (z > 1 && x < 7) {
            if (arr[(z-2)*8 + x+1] == 0)
                moves.Add(new Move(z, x, z - 2, x + 1, 200, 0));
            else
                if (arr[start] > 0 && arr[(z-2)*8 + x+1] < 0 || arr[start] < 0 && arr[(z-2)*8 + x+1] > 0)
                    moves.Add(new Move(z, x, z - 2, x + 1, PieceValues(arr[start]), PieceValues(arr[(z-2)*8 + (x+1)])));
        }
        return moves;
    }
    private List<Move> Rook(int[] arr, bool color, int start) {
        List<Move> moves = new List<Move>();
        int z = start / 8;
        int x = start % 8;
        for (int a = 1; a < 8; a++) {
            if (z + a <= 7) {
                if (arr[(z+a)*8 + x] == 0)
                    moves.Add(new Move(z, x, z + a, x, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x] < 0 || arr[start] < 0 && arr[(z+a)*8 + x] > 0)
                        moves.Add(new Move(z, x, z + a, x, PieceValues(arr[start]), PieceValues(arr[(z+a)*8 + (x)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z - a >= 0) {
                if (arr[(z-a)*8 + x] == 0)
                    moves.Add(new Move(z, x, z - a, x, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x] < 0 || arr[start] < 0 && arr[(z-a)*8 + x] > 0)
                        moves.Add(new Move(z, x, z - a, x, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (x + a <= 7) {
                if (arr[(z)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z)*8 + x + a] < 0 || arr[start] < 0 && arr[(z)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z, x + a, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (x - a >= 0) {
                if (arr[(z)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z)*8 + x - a] < 0 || arr[start] < 0 && arr[(z)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z, x - a, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        return moves;
    }
    private List<Move> Bishop(int[] arr, bool color, int start) {
        List<Move> moves = new List<Move>();
        int z = start / 8;
        int x = start % 8;
        for (int a = 1; a < 8; a++) {
            if (z + a <= 7 && x + a <= 7) {
                if (arr[(z+a)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z + a, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x + a] < 0 || arr[start] < 0 && arr[(z+a)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z + a, x + a, PieceValues(arr[start]), PieceValues(arr[(z+a)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z - a >= 0 && x - a >= 0) {
                if (arr[(z-a)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z - a, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x - a] < 0 || arr[start] < 0 && arr[(z-a)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z - a, x - a, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z - a >= 0 && x + a <= 7) {
                if (arr[(z - a)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z - a, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x + a] < 0 || arr[start] < 0 && arr[(z-a)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z-a, x + a, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z + a <= 7 && x - a >= 0) {
                if (arr[(z+a)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z+a, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x - a] < 0 || arr[start] < 0 && arr[(z+a)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z + a, x - a, PieceValues(arr[start]), PieceValues(arr[(z + a)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        return moves;
    }
    private List<Move> Queen(int[] arr, bool color, int start) {
        List<Move> moves = new List<Move>();
        int z = start / 8;
        int x = start % 8;
        for (int a = 1; a < 8; a++) {
            if (z + a <= 7) {
                if (arr[(z+a)*8 + x] == 0)
                    moves.Add(new Move(z, x, z + a, x, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x] < 0 || arr[start] < 0 && arr[(z+a)*8 + x] > 0)
                        moves.Add(new Move(z, x, z + a, x, PieceValues(arr[start]), PieceValues(arr[(z+a)*8 + (x)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z - a >= 0) {
                if (arr[(z-a)*8 + x] == 0)
                    moves.Add(new Move(z, x, z - a, x, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x] < 0 || arr[start] < 0 && arr[(z-a)*8 + x] > 0)
                        moves.Add(new Move(z, x, z - a, x, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (x + a <= 7) {
                if (arr[(z)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z)*8 + x + a] < 0 || arr[start] < 0 && arr[(z)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z, x + a, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (x - a >= 0) {
                if (arr[(z)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z)*8 + x - a] < 0 || arr[start] < 0 && arr[(z)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z, x - a, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z + a <= 7 && x + a <= 7) {
                if (arr[(z+a)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z + a, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x + a] < 0 || arr[start] < 0 && arr[(z+a)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z + a, x + a, PieceValues(arr[start]), PieceValues(arr[(z+a)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z - a >= 0 && x - a >= 0) {
                if (arr[(z-a)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z - a, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x - a] < 0 || arr[start] < 0 && arr[(z-a)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z - a, x - a, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z - a >= 0 && x + a <= 7) {
                if (arr[(z - a)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z - a, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x + a] < 0 || arr[start] < 0 && arr[(z-a)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z-a, x + a, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 8; a++) {
            if (z + a <= 7 && x - a >= 0) {
                if (arr[(z+a)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z+a, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x - a] < 0 || arr[start] < 0 && arr[(z+a)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z + a, x - a, PieceValues(arr[start]), PieceValues(arr[(z + a)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        return moves;
    }
    public List<Move> King(int[] arr, bool color, int start) {
        List<Move> moves = new List<Move>();
        int z = start / 8;
        int x = start % 8;
        for (int a = 1; a < 2; a++) {
            if (z + a <= 7) {
                if (arr[(z+a)*8 + x] == 0)
                    moves.Add(new Move(z, x, z + a, x, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x] < 0 || arr[start] < 0 && arr[(z+a)*8 + x] > 0)
                        moves.Add(new Move(z, x, z + a, x, PieceValues(arr[start]), PieceValues(arr[(z+a)*8 + (x)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (z - a >= 0) {
                if (arr[(z-a)*8 + x] == 0)
                    moves.Add(new Move(z, x, z - a, x, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x] < 0 || arr[start] < 0 && arr[(z-a)*8 + x] > 0)
                        moves.Add(new Move(z, x, z - a, x, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (x + a <= 7) {
                if (arr[(z)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z)*8 + x + a] < 0 || arr[start] < 0 && arr[(z)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z, x + a, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (x - a >= 0) {
                if (arr[(z)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z)*8 + x - a] < 0 || arr[start] < 0 && arr[(z)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z, x - a, PieceValues(arr[start]), PieceValues(arr[(z)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (z + a <= 7 && x + a <= 7) {
                if (arr[(z+a)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z + a, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x + a] < 0 || arr[start] < 0 && arr[(z+a)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z + a, x + a, PieceValues(arr[start]), PieceValues(arr[(z+a)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (z - a >= 0 && x - a >= 0) {
                if (arr[(z-a)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z - a, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x - a] < 0 || arr[start] < 0 && arr[(z-a)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z - a, x - a, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (z - a >= 0 && x + a <= 7) {
                if (arr[(z - a)*8 + x + a] == 0)
                    moves.Add(new Move(z, x, z - a, x + a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z-a)*8 + x + a] < 0 || arr[start] < 0 && arr[(z-a)*8 + x + a] > 0)
                        moves.Add(new Move(z, x, z-a, x + a, PieceValues(arr[start]), PieceValues(arr[(z-a)*8 + (x + a)])));
                    break;
                }
            }
            else
                break;
        }
        for (int a = 1; a < 2; a++) {
            if (z + a <= 7 && x - a >= 0) {
                if (arr[(z+a)*8 + x - a] == 0)
                    moves.Add(new Move(z, x, z+a, x - a, 200, 0));
                else {
                    if (arr[start] > 0 && arr[(z+a)*8 + x - a] < 0 || arr[start] < 0 && arr[(z+a)*8 + x - a] > 0)
                        moves.Add(new Move(z, x, z + a, x - a, PieceValues(arr[start]), PieceValues(arr[(z + a)*8 + (x - a)])));
                    break;
                }
            }
            else
                break;
        }
        if (Mathf.Abs(arr[start]) > 100 && Mathf.Abs(arr[z*8]) == 103) {
            bool bad = false;
            for (int a = 0; a <= 2; a++) {
                if (arr[z*8 + z - a] != 0 || danger(arr, color, z, x - a) == true) {
                    bad = true;
                    break;
                }
            }
            if (bad == false) {
                Move temp = new Move(z, x, z, 2, 200, 0);
                temp.castleLeft(z);
                moves.Add(temp);
            }
        }
        if (Mathf.Abs(arr[start]) > 100 && Mathf.Abs(arr[z*8 + 7]) == 103) {
            bool bad = false;
            for (int a = 0; a <= 2; a++) {
                if (arr[z*8 + z + a] != 0 || danger(arr, color, z, x + a) == true) {
                    bad = true;
                    break;
                }
            }
            if (bad == false) {
                Move temp = new Move(z, x, z, 6, 200, 0);
                temp.castleRight(z);
                moves.Add(temp);
            }
        }
        return moves;
    }
    public bool danger(int[] arr, bool color, int ez, int ex) {
        for (int a = 0; a < 64; a++) {
            List<Move> temp = new List<Move>();
            if (Mathf.Abs(arr[a]) % 100 == 1 && (color == true && arr[a] < 0 || color == false && arr[a] > 0))
                temp = Pawn(arr, color, a);
            if (Mathf.Abs(arr[a]) % 100 == 2 && (color == true && arr[a] < 0 || color == false && arr[a] > 0))
                temp = Knight(arr, color, a);
            if (Mathf.Abs(arr[a]) % 100 == 3 && (color == true && arr[a] < 0 || color == false && arr[a] > 0))
                temp = Rook(arr, color, a);
            if (Mathf.Abs(arr[a]) % 100 == 4 && (color == true && arr[a] < 0 || color == false && arr[a] > 0))
                temp = Bishop(arr, color, a);
            if (Mathf.Abs(arr[a]) % 100 == 5 && (color == true && arr[a] < 0 || color == false && arr[a] > 0))
                temp = Queen(arr, color, a);
            for (int x = 0; x < temp.Count; x++)
                if (temp[x].endz == ez && temp[x].endx == ex) {
                    return true;
                }
        }
        return false;
    }
    private int PieceValues(int piece) {
        int points = 0;
        if (Mathf.Abs(piece) % 100 == 1) {
            points += 1;
        }
        if (Mathf.Abs(piece) % 100 == 2) {
            points += 9;
        }
        if (Mathf.Abs(piece) % 100 == 3) {
            points += 15;
        }
        if (Mathf.Abs(piece) % 100 == 4) {
            points += 12;
        }
        if (Mathf.Abs(piece) % 100 == 5) {
            points += 20;
        }
        if (Mathf.Abs(piece) % 100 == 6) {
            points += 100;
        }
        return points;
    }
}
