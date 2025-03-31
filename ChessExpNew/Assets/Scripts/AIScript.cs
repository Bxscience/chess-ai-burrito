using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class AIScript
{
    public int depth = 5;
    public Dictionary<GameObject[,], int> table = new Dictionary<GameObject[,], int>();
    public Stack<GameObject> undo_object = new Stack<GameObject>();
    
    int Evaluation(GameObject[,] arr, bool color) { // basic evaluation function // this is the new one for negamax
        if (table.ContainsKey(arr))
            return table[arr];
        int points = 0;
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z,x] != null) {
                if (arr[z,x].GetComponent<Pawn>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == color) {
                        points += 10;
                        points += 4 * ((int) (arr[z,x].transform.position.z + 3.5));
                        points += 2 * (Mathf.Abs(4 - (int) (arr[z,x].transform.position.x + 3.5)));
                    }
                    else {
                        points -= 10;
                        points -= 2 * ((int) (arr[z,x].transform.position.z + 3.5));
                        points -= 1 * (Mathf.Abs(4 - (int) (arr[z,x].transform.position.x + 3.5)));
                    }
                }
                if (arr[z,x].GetComponent<Knight>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == color)
                        points += 30;
                    else
                        points -= 30;
                }
                if (arr[z,x].GetComponent<Rook>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == color)
                        points += 100;
                    else
                        points -= 100;
                }
                if (arr[z,x].GetComponent<Bishop>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == color)
                        points += 50;
                    else
                        points -= 50;
                }
                if (arr[z,x].GetComponent<Queen>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == color)
                        points += 200;
                    else
                        points -= 200;
                }
                if (arr[z,x].GetComponent<King>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == color)
                        points += 300;
                    else
                        points -= 300;
                }
                }
            }
        }
        table.Add(arr, points);
        return points;
    }
    public Move RandomMove(GameObject[,] arr, bool white) {
        System.Random rand = new System.Random();
        List<Move> all_moves = new List<Move>();
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z, x] != null) {
                    if (arr[z, x].GetComponent<Piece>().isWhite == white && arr[z, x].GetComponent<Piece>().GameManager.GetComponent<GameManager>().p2_check != true)
                        all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                    if (arr[z, x].GetComponent<Piece>().isWhite == white && arr[z, x].GetComponent<Piece>().GameManager.GetComponent<GameManager>().p2_check == true && arr[z, x].GetComponent<King>() == true)
                        all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                }
            }
        }
        return all_moves[rand.Next(0, all_moves.Count)]; 
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
    //public List<Move> 
    public Move BestMove(GameObject[,] arr, bool white) { // calcsulates the best move
        //undo_object.Clear();
        int bestScore = int.MinValue;
        int index = 0;
        List<Move> all_moves = new List<Move>();
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z, x] != null) {
                    if (arr[z, x].GetComponent<Piece>().isWhite == white && arr[z, x].GetComponent<Piece>().GameManager.GetComponent<GameManager>().p2_check != true)
                        all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                    if (arr[z, x].GetComponent<Piece>().isWhite == white && arr[z, x].GetComponent<Piece>().GameManager.GetComponent<GameManager>().p2_check == true && arr[z, x].GetComponent<King>() == true)
                        all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                }
            }
        }
        all_moves = MVVLVA(all_moves);
        
        for (int a = 0; a < all_moves.Count; a++) {
            GameObject[,] arr2 = new GameObject[8,8];
                for (int z = 0; z < 8; z++) 
                    for (int x = 0; x < 8; x++)
                        arr2[z,x] = arr[z,x];
            makeMove(arr2, all_moves[a]);
            int score = Negamax(arr2, depth - 1, int.MinValue, int.MaxValue, white);
            undoMove(arr2, all_moves[a]);
            if (score > bestScore) { // get best score
                bestScore = score;
                index = a;
            }
        }
        /*
        for (int a = 0; a < all_moves.Count; a++) {
            GameObject[,] arr2 = new GameObject[8,8];
            for (int z = 0; z < 8; z++) 
                for (int x = 0; x < 8; x++)
                    arr2[z,x] = arr[z,x];
            arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
            arr2[all_moves[a].startz, all_moves[a].startx] = null; // all above is precedent for the minimax function
            int score = Negamax(arr2, depth - 1, int.MinValue, int.MaxValue, white);
            if (score > bestScore) { // get best score
                bestScore = score;
                index = a;
            }
        }
        */
        return all_moves[index];
    }
    /*
    int Minimax(GameObject[,] arr, int depth, int alpha, int beta, bool maximize, bool color) {
        if (depth == 0) {
            return Evaluation(arr);
        }
        
        
        if (maximize) {
            int maxEval = int.MinValue;
            List<Move> all_moves = new List<Move>();
            for (int z = 0; z < 8; z++) {
                for (int x = 0; x < 8; x++) {
                    if (arr[z, x] != null) {
                        if (arr[z, x].GetComponent<Piece>().isWhite == color) // get all moves of the pieces of the AIs color
                            all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                    }
                }
            }

            for (int a = 0; a < all_moves.Count; a++) {
                GameObject[,] arr2 = new GameObject[8,8];
                for (int z = 0; z < 8; z++) 
                    for (int x = 0; x < 8; x++)
                        arr2[z,x] = arr[z,x];
                if (all_moves[a].endz > 7 || all_moves[a].endz < 0 || all_moves[a].endx < 0 || all_moves[a].endx > 7)
                    break;
                arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
                arr2[all_moves[a].startz, all_moves[a].startx] = null;
                int eval = Minimax(arr2, depth - 1, int.MinValue, int.MaxValue, false, color); // recursive call
                maxEval = Mathf.Max(maxEval, eval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha) {
                    break; // alpha beta pruning
                }
            }
            return maxEval;
        }
        else {
            int minEval = int.MaxValue;
            List<Move> all_moves = new List<Move>();
            for (int z = 0; z < 8; z++) {
                for (int x = 0; x < 8; x++) {
                    if (arr[z, x] != null) {
                        if (arr[z, x].GetComponent<Piece>().isWhite == !color) // get moves of opposing team
                            all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                    }
                }
            }
            for (int a = 0; a < all_moves.Count; a++) {
                GameObject[,] arr2 = new GameObject[8,8];
                for (int z = 0; z < 8; z++) 
                    for (int x = 0; x < 8; x++)
                        arr2[z,x] = arr[z,x];
                if (all_moves[a].endz > 7 || all_moves[a].endz < 0 || all_moves[a].endx < 0 || all_moves[a].endx > 7)
                    break;
                arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
                arr2[all_moves[a].startz, all_moves[a].startx] = null;
                int eval = Minimax(arr2, depth - 1, alpha, beta, true, color);
                minEval = Mathf.Min(minEval, eval);
                beta = Mathf.Min(beta, eval);

                if (beta <= alpha) {
                    break; // Alpha cut-off
                }
            }
            return minEval;
        }
    }
    */
    int Negamax(GameObject[,] arr, int depth, int alpha, int beta, bool color) {
        if (depth == 0) {
            if (color == false)
                return Evaluation(arr, color);
            else
                return -Evaluation(arr, color);
        }
        List<Move> all_moves = new List<Move>();
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z, x] != null) {
                    if (arr[z, x].GetComponent<Piece>().isWhite == color) // get all moves of the pieces of the AIs color
                        all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                }
            }
        }
        
        all_moves = MVVLVA(all_moves);
        int maxEval = int.MinValue;
        
        for (int a = 0; a < all_moves.Count; a++) {
            if (all_moves[a].endz > 7 || all_moves[a].endz < 0 || all_moves[a].endx < 0 || all_moves[a].endx > 7)
                break;
            makeMove(arr, all_moves[a]);
            maxEval = Mathf.Max(maxEval, -Negamax(arr, depth - 1, -beta, -alpha, !color));
            alpha = Mathf.Max(alpha, maxEval);
            undoMove(arr, all_moves[a]);
            if (beta <= alpha) {
                break; // Alpha cut-off
            }
        }
        /*
        for (int a = 0; a < all_moves.Count; a++) {
            GameObject[,] arr2 = new GameObject[8,8];
            for (int z = 0; z < 8; z++) 
                for (int x = 0; x < 8; x++)
                    arr2[z,x] = arr[z,x];
            if (all_moves[a].endz > 7 || all_moves[a].endz < 0 || all_moves[a].endx < 0 || all_moves[a].endx > 7)
                break;
            arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
            arr2[all_moves[a].startz, all_moves[a].startx] = null;
            maxEval = Mathf.Max(maxEval, -Negamax(arr2, depth - 1, -beta, -alpha, !color));
            alpha = Mathf.Max(alpha, maxEval);

            if (beta <= alpha) {
                break; // Alpha cut-off
            }
        }
        */
        return maxEval;
    }
    public void makeMove(GameObject[,] arr, Move move) {
        undo_object.Push(arr[move.endz, move.endx]);
        arr[move.endz, move.endx] = arr[move.startz, move.startx];
        arr[move.startz, move.startx] = null;
    }
    public void undoMove(GameObject[,] arr, Move move) {
        arr[move.startz, move.startx] = arr[move.endz, move.endx];
        arr[move.endz, move.endx] = undo_object.Peek();
        undo_object.Pop();
    }
}
