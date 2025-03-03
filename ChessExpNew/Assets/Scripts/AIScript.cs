using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIScript
{
    public int depth = 3;

    void Start() {
    }
    
    int Evaluation(GameObject[,] arr) {
        int points = 0;
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z,x] != null) {
                if (arr[z,x].GetComponent<Pawn>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == false)
                        points += 10;
                    else
                        points -= 10;
                }
                if (arr[z,x].GetComponent<Knight>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == false)
                        points += 30;
                    else
                        points -= 30;
                }
                if (arr[z,x].GetComponent<Rook>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == false)
                        points += 50;
                    else
                        points -= 50;
                }
                if (arr[z,x].GetComponent<Bishop>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == false)
                        points += 50;
                    else
                        points -= 50;
                }
                if (arr[z,x].GetComponent<Queen>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == false)
                        points += 200;
                    else
                        points -= 200;
                }
                if (arr[z,x].GetComponent<King>() != null) {
                    if (arr[z,x].GetComponent<Piece>().isWhite == true)
                        points += 300;
                    else
                        points -= 300;
                }
                }
            }
        }
        return points;
    }
    public Move BestMove(GameObject[,] arr, bool white) {
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
        for (int a = 0; a < all_moves.Count; a++) {
            GameObject[,] arr2 = new GameObject[8,8];
            for (int z = 0; z < 8; z++) 
                for (int x = 0; x < 8; x++)
                    arr2[z,x] = arr[z,x];
            arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
            arr2[all_moves[a].startz, all_moves[a].startx] = null;
            int score = Minimax(arr2, depth - 1, int.MinValue, int.MaxValue, false, white);
            if (score > bestScore) {
                bestScore = score;
                index = a;
            }
        }
        return all_moves[index];
    }
    int Minimax(GameObject[,] arr, int depth, int alpha, int beta, bool maximize, bool color) {
        if (depth == 0) {
            return Evaluation(arr);
        }
        List<Move> all_moves = new List<Move>();
        for (int z = 0; z < 8; z++) {
            for (int x = 0; x < 8; x++) {
                if (arr[z, x] != null) {
                    if (arr[z, x].GetComponent<Piece>().isWhite == color)
                        all_moves.AddRange(arr[z, x].GetComponent<Piece>().getAllMoves(arr));
                }
            }
        }
        if (maximize) {
            int maxEval = int.MinValue;
            for (int a = 0; a < all_moves.Count; a++) {
                GameObject[,] arr2 = new GameObject[8,8];
                for (int z = 0; z < 8; z++) 
                    for (int x = 0; x < 8; x++)
                        arr2[z,x] = arr[z,x];
                arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
                arr2[all_moves[a].startz, all_moves[a].startx] = null;
                int eval = Minimax(arr2, depth - 1, int.MinValue, int.MaxValue, false, color);
                maxEval = Mathf.Max(maxEval, eval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }
            return maxEval;
        }
        else {
            int minEval = int.MaxValue;
            for (int a = 0; a < all_moves.Count; a++) {
                GameObject[,] arr2 = new GameObject[8,8];
                for (int z = 0; z < 8; z++) 
                    for (int x = 0; x < 8; x++)
                        arr2[z,x] = arr[z,x];
                arr2[all_moves[a].endz, all_moves[a].endx] = arr2[all_moves[a].startz, all_moves[a].startx];
                arr2[all_moves[a].startz, all_moves[a].startx] = null;
                int eval = Minimax(arr2, depth - 1, alpha, beta, true, color);
                minEval = Mathf.Min(minEval, eval);
                beta = Mathf.Min(beta, eval);

                if (beta <= alpha)
                    break; // Alpha cut-off
            }
            return minEval;
        }
    }
}
