using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class AIScript
{
    public int depth = GameSlider.game_depth;

    public Stack<int> undo_tan = new Stack<int>();
    public Stack<int> undo_tar = new Stack<int>();
    public Stack<int> undo_en = new Stack<int>();
    public IntConverter intrep = new IntConverter();
    public TranspositionTable table = new TranspositionTable();
    public Zorbist zorbist = new Zorbist(); 
    public GameObject GameManager = new GameObject();
    
    void Start() {
        GameManager = GameObject.Find("GameManager");
    }
    int Evaluation(int[] arr, bool color, List<Move> all_moves) {
        GameManager = GameObject.Find("GameManager");
        int pieces = 0;
            for (int a = 0; a < 64; a++)
                if (arr[a] != 0)
                    pieces++;
        if (pieces < 10)
            return EndGame(arr, color, all_moves);
        if (GameManager.GetComponent<GameManager>().turn <= 40) {
            return EarlyGame(arr, color, all_moves);
        }
        return MidGame(arr, color, all_moves);
    }
    int EarlyGame(int[] arr, bool color, List<Move> all_moves) { // midgame eval function
        int points = 0;
        for (int a = 0; a < 64; a++) {
            if (arr[a] != 0) {
            if (Mathf.Abs(arr[a]) % 100 == 1) {
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0) {
                    points += 10;
                }
                else {
                    points -= 10;
                }
            }
            if (Mathf.Abs(arr[a]) % 100 == 2)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 30;
                else
                    points -= 30;

            if (Mathf.Abs(arr[a]) % 100 == 3)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 100;
                else
                    points -= 100;
            if (Mathf.Abs(arr[a]) % 100 == 4)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 50;
                else
                    points -= 50;
            if (Mathf.Abs(arr[a]) % 100 == 5)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 200;
                else
                    points -= 200;
            if (Mathf.Abs(arr[a]) % 100 == 6)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0) {
                    points += 300;
                }
                else {
                    points -= 300;
                }
            }
        }
        return points;
    }
    int MidGame(int[] arr, bool color, List<Move> all_moves) { // midgame eval function
        int points = all_moves.Count;
        for (int a = 0; a < 64; a++) {
            if (arr[a] != 0) {
            if (Mathf.Abs(arr[a]) % 100 == 1) {
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0) {
                    points += 10;
                    points += 4 * (a/8);
                    points += 2 * (Mathf.Abs(4 - a % 8));
                    if (a % 8 > 0 && arr[a-1] == arr[a])
                        points += 2;
                    if (a % 8 < 7 && arr[a+1] == arr[a])
                        points += 2;
                    for (int z = 0; z < 8; z++) {
                        if (arr[z*8 + a%8] == arr[a])
                            points -= 4;
                    }
                    for (int z = a/8; z < 8; z++) {
                        if (a % 8 > 0 && arr[z*8 + a % 8 - 1] == arr[a])
                            points -= 2;
                        if (a % 8 < 7 && arr[z*8 + a % 8 + 1] == arr[a])
                            points -= 2;  
                    }
                }
                else {
                    points -= 10;
                    points -= 4 * (8 - (a/8));
                    points -= 2 * (Mathf.Abs(4 - a % 8));
                    if (a % 8 > 0 && arr[a-1] == arr[a])
                        points -= 2;
                    if (a % 8 < 7 && arr[a+1] == arr[a])
                        points -= 2;
                    for (int z = 0; z < 8; z++) {
                        if (arr[z*8 + a%8] == arr[a])
                            points += 2;
                    }
                    for (int z = a/8; z >= 0; z--) {
                        if (a % 8 > 0 && arr[z*8 + a % 8 - 1] == arr[a])
                            points += 2;
                        if (a % 8 < 7 && arr[z*8 + a % 8 + 1] == arr[a])
                            points += 2;  
                    }
                }
            }
            if (Mathf.Abs(arr[a]) % 100 == 2)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 30;
                else
                    points -= 30;

            if (Mathf.Abs(arr[a]) % 100 == 3)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 100;
                else
                    points -= 100;
            if (Mathf.Abs(arr[a]) % 100 == 4)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 50;
                else
                    points -= 50;
            if (Mathf.Abs(arr[a]) % 100 == 5)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 200;
                else
                    points -= 200;
            if (Mathf.Abs(arr[a]) % 100 == 6)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0) {
                    points += 300;
                    if (intrep.danger(arr, color, a/8, a%8))
                        points -= 100;
                }
                else {
                    points -= 300;
                    if (intrep.danger(arr, !color, a/8, a%8))
                        points += 100;
                }
            }
        }
        return points;
    }
    int EndGame(int[] arr, bool color, List<Move> all_moves) { // midgame eval function
        int points = all_moves.Count;
        for (int a = 0; a < 64; a++) {
            if (arr[a] != 0) {
            if (Mathf.Abs(arr[a]) % 100 == 1) {
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0) {
                    points += 10;
                    points += 8 * (a/8);
                }
                else {
                    points -= 10;
                    points -= 8 * (8 - (a/8));
                }
            }
            if (Mathf.Abs(arr[a]) % 100 == 2)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 30;
                else
                    points -= 30;

            if (Mathf.Abs(arr[a]) % 100 == 3)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 100;
                else
                    points -= 100;
            if (Mathf.Abs(arr[a]) % 100 == 4)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 50;
                else
                    points -= 50;
            if (Mathf.Abs(arr[a]) % 100 == 5)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0)
                    points += 500;
                else
                    points -= 500;
            if (Mathf.Abs(arr[a]) % 100 == 6)
                if (color == true && arr[a] > 0 || color == false && arr[a] < 0) {
                    points += 600;
                    points += intrep.King(arr, color, a).Count * 20;
                    points += (Mathf.Abs(a/8 - 4) + Mathf.Abs(a%8 - 4)) * 10;
                    if (intrep.danger(arr, color, a/8, a%8))
                        points -= 200;
                }
                else {
                    points -= 600;
                    points -= intrep.King(arr, color, a).Count * 20;
                    points -= (Mathf.Abs(a/8 - 4) + Mathf.Abs(a%8 - 4)) * 10;
                    if (intrep.danger(arr, !color, a/8, a%8))
                        points += 200;
                }
            }
        }
        return points;
    }
    public List<Move> MVVLVA(List<Move> all_moves) {
        bool all0 = true;
        for (int x = 0; x < all_moves.Count; x++) {
            if (all_moves[x].MVVLVA != -200)
                all0 = false;
        }
        if (all0 == true)
            return all_moves;
        List<int> keys = new List<int>();
        for (int x = 0; x < all_moves.Count; x++) {
            keys.Add(all_moves[x].MVVLVA);
        }
        Array.Sort(keys.ToArray(), all_moves.ToArray());
        all_moves = new List<Move>(all_moves);
        all_moves.Reverse();
        return all_moves;
    }
    public Move BestMove(GameObject[,] arr, bool white) { // calculates the best move
        int time = DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second; 
        int bestScore = int.MinValue;
        int index = 0;
        List<Move> all_moves = new List<Move>();
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z, x] != null) {
                    Debug.Log("not null");
                    if (arr[z, x].GetComponent<Piece>().isWhite == white)
                        all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                }
            }
        }
        Debug.Log(all_moves.Count + " all moves");
        //all_moves = MVVLVA(all_moves);
        for (int a = 0; a < all_moves.Count; a++) {
            //GameObject[,] arr2 = new GameObject[8,8];
            //for (int z = 0; z < 8; z++) 
                //for (int x = 0; x < 8; x++)
                    //arr2[z,x] = arr[z,x];
            int[] arr3 = intrep.convert(arr);
            //arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
            //arr2[all_moves[a].startz, all_moves[a].startx] = null; // all above is precedent for the minimax function
            makeMove(arr3, all_moves[a]);
            int score = Minimax(arr3, depth - 1, int.MinValue, int.MaxValue, false, white);
            undoMove(arr3, all_moves[a]);
            if (score > bestScore) { // get best score
                bestScore = score;
                index = a;
            }
        }
        Debug.Log(DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second - time);
        return all_moves[index];
    }
    int Minimax(int[] arr, int depth, int alpha, int beta, bool maximize, bool color) {
        
        ulong hash = zorbist.Hash(arr, maximize);
        Entry entry = table.GetEntry(hash, depth);
        
        if (entry != null && entry.depth >= depth) {
            return entry.evaluation;
        }
        
        List<Move> all_moves = intrep.getMoves(arr, !maximize);

        if (depth == 0) {
            return Evaluation(arr, color, all_moves);
        }

        
        if (entry != null && entry.move != null) {
            all_moves.Remove(entry.move);
            all_moves.Insert(0, entry.move); 
        }
        

        Move bestMove = null;
        int finalEval = 0;

        if (maximize) {
            finalEval = int.MinValue;
            for (int a = 0; a < all_moves.Count; a++) {
                if (all_moves[a].endz > 7 || all_moves[a].endz < 0 || all_moves[a].endx < 0 || all_moves[a].endx > 7
                || all_moves[a].startz > 7 || all_moves[a].startz < 0 || all_moves[a].startx < 0 || all_moves[a].startx > 7)
                    break;
                makeMove(arr, all_moves[a]);
                int eval = Minimax(arr, depth - 1, alpha, beta, false, color); // recursive call
                undoMove(arr, all_moves[a]);
                if (eval > finalEval) {
                    finalEval = eval;
                    bestMove = all_moves[a];
                }
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha) {
                    break; // alpha beta pruning
                }
            }
            //table.StoreEntry(hash, depth, finalEval, bestMove, type);
            return finalEval;
        }
        else {
            finalEval = int.MaxValue;
            //all_moves = MVVLVA(all_moves);
            for (int a = 0; a < all_moves.Count; a++) {
                if (all_moves[a].endz > 7 || all_moves[a].endz < 0 || all_moves[a].endx < 0 || all_moves[a].endx > 7
                || all_moves[a].startz > 7 || all_moves[a].startz < 0 || all_moves[a].startx < 0 || all_moves[a].startx > 7)
                    break;
                arr = makeMove(arr, all_moves[a]);
                int eval = Minimax(arr, depth - 1, alpha, beta, true, color);
                arr = undoMove(arr, all_moves[a]);
                if (eval < finalEval) {
                    finalEval = eval;
                    bestMove = all_moves[a];
                }
                beta = Mathf.Min(beta, eval);

                if (beta <= alpha) {
                    break; // Alpha cut-off
                }
            }
            int type = 0;
            if (finalEval <= alpha)
                type = 1;
            if (finalEval >= beta)
                type = 2;
            table.StoreEntry(hash, depth, finalEval, bestMove, type);
            return finalEval;
        }
    }
    public int[] makeMove(int[] arr, Move move) {
        bool no_passant = true;
        int X = 0;
        for (int x = 0; x < 64; x++) {
            if (Mathf.Abs(arr[x]) > 200) {
                no_passant = false;
                X = x;
                arr[x] %= 200;
                break;
            }
        }
        if (no_passant == true) {
            undo_en.Push(10000);
        }
        else {
            undo_en.Push(X);
        }
        
        if (Mathf.Abs(arr[GetIndex(move.startz, move.startx)]) == 1 && move.endz != move.startz && move.endx != move.startx && 
        arr[GetIndex(move.endz, move.endx)] == 0) {
            move.passant_flag = true;
        }
        if (move.passant_flag == true)
            undo_tan.Push(arr[GetIndex(move.startz, move.endx)]);
        else
            undo_tan.Push(arr[GetIndex(move.endz, move.endx)]);
        undo_tar.Push(arr[GetIndex(move.startz, move.startx)]);
        arr[GetIndex(move.endz, move.endx)] = arr[GetIndex(move.startz, move.startx)];
        if (arr[GetIndex(move.endz, move.endx)] > 100)
            arr[GetIndex(move.endz, move.endx)] -= 100;
        if (arr[GetIndex(move.endz, move.endx)] < -100)
            arr[GetIndex(move.endz, move.endx)] += 100;
        
        if (Mathf.Abs(arr[GetIndex(move.endz, move.endx)]) == 1 && Mathf.Abs(move.endz - move.startz) == 2) {
            if (arr[GetIndex(move.endz, move.endx)] > 0)
                arr[GetIndex(move.endz, move.endx)] += 200;
            if (arr[GetIndex(move.endz, move.endx)] < 0)
                arr[GetIndex(move.endz, move.endx)] -= 200;
        }
        arr[GetIndex(move.startz, move.startx)] = 0;
        if (move.passant_flag == true) {
            arr[GetIndex(move.startz, move.endx)] = 0;
        }
        
        if (move.castleleft == true) {
            arr[GetIndex(0, 3)] = arr[GetIndex(0, 0)] % 100;
            arr[GetIndex(0, 0)] = 0;
        }
        if (move.castleright == true) {
            arr[GetIndex(0, 5)] = arr[GetIndex(0, 7)] % 100;
            arr[GetIndex(0, 7)] = 0;
        }
        if (move.promotion != 0) {
            if (move.promotion == 1) {
                arr[GetIndex(move.endz, move.endx)] *= 2;
            }
            if (move.promotion == 2) {
                arr[GetIndex(move.endz, move.endx)] *= 3;
            }
            if (move.promotion == 3) {
                arr[GetIndex(move.endz, move.endx)] *= 4;
            }
            if (move.promotion == 4) {
                arr[GetIndex(move.endz, move.endx)] *= 5;
            }
        }
        return arr;
    }
    public int[] undoMove(int[] arr, Move move) {
        if (undo_en.Count != 0 && undo_en.Peek() != 10000) {
            if (arr[undo_en.Peek()] > 0)
                arr[undo_en.Peek()] += 200;
            else
                arr[undo_en.Peek()] -= 200;
        }
        if (undo_en.Count != 0)
            undo_en.Pop();
        if (move.passant_flag == true) {
            arr[GetIndex(move.endz, move.endx)] = 0;
            arr[GetIndex(move.startz, move.startx)] = undo_tar.Pop();
            arr[GetIndex(move.startz, move.endx)] = undo_tan.Pop();
        }
        else {
            arr[GetIndex(move.startz, move.startx)] = undo_tar.Pop();
            arr[GetIndex(move.endz, move.endx)] = undo_tan.Pop();
        }
        
        if (move.castleleft == true) {
            arr[GetIndex(0, 0)] = 103;
            arr[GetIndex(0, 3)] = 0;
        }
        if (move.castleright == true) {
            arr[GetIndex(0, 5)] = 0;
            arr[GetIndex(0, 7)] = 103;
        }
        
        return arr;
    }
    public int GetIndex(int z, int x) {
        return z * 8 + x;
    }
    public bool TestEqual(GameObject[,] arr, int[] arr2) {
        bool equal = true;
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z,x] == null && arr2[z*8 + x] != 0) {
                    equal = false;
                    break;
                }
                if (arr[z,x] != null) {
                    if (arr[z,x].GetComponent<Pawn>() != null && Mathf.Abs(arr2[z*8+x]) % 100 != 1) {
                        equal = false;
                        break;
                    }
                    if (arr[z,x].GetComponent<Knight>() != null && Mathf.Abs(arr2[z*8+x]) % 100 != 2) {
                        equal = false;
                        break;
                    }
                    if (arr[z,x].GetComponent<Rook>() != null && Mathf.Abs(arr2[z*8+x]) % 100 != 3) {
                        equal = false;
                        break;
                    }
                    if (arr[z,x].GetComponent<Bishop>() != null && Mathf.Abs(arr2[z*8+x]) % 100 != 4) {
                        equal = false;
                        break;
                    }
                    if (arr[z,x].GetComponent<Queen>() != null && Mathf.Abs(arr2[z*8+x]) % 100 != 5) {
                        equal = false;
                        break;
                    }
                    if (arr[z,x].GetComponent<King>() != null && Mathf.Abs(arr2[z*8+x]) % 100 != 6) {
                        equal = false;
                        break;
                    }
                }
            }
        }
        return equal;
    }
    public void TestMovesEqual(List<Move> list1, List<Move> list2) {
        String str1 = "str 1";
        String str2 = "str 2";
        for (int a = 0; a < list1.Count; a++) {
            str1 += ("\n" + list1[a].startz + " " + list1[a].startx + " -> " + list1[a].endz + " " + list1[a].endx);
        }
        for (int a = 0; a < list2.Count; a++) {
            str2 += ("\n" + list2[a].startz + " " + list2[a].startx + " -> " + list2[a].endz + " " + list2[a].endx);
        }
        Debug.Log(str1);
        Debug.Log(str2);
        Debug.Log("difference?: " + (list2.Count - list1.Count));
    }
    public void PrintArray(GameObject[,] arr) {
        String str = "GameObjects:";
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                str += ("\n" + arr[z,x]);
            }
        }
        Debug.Log(str);
    }
    public void PrintArray(int[] arr) {
        String str = "Ints:";
        for (int x = 0; x < 64; x++) {
            str += ("\n" + arr[x]);
        }
        Debug.Log(str);
    }
}
