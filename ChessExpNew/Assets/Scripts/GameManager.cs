using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pawnw;
    public GameObject rookw;
    public GameObject knightw;
    public GameObject bishopw;
    public GameObject queenw;
    public GameObject kingw;

    public GameObject pawnb;
    public GameObject rookb;
    public GameObject knightb;
    public GameObject bishopb;
    public GameObject queenb;
    public GameObject kingb;

    public int turn;
    public bool f_ply;
    public Object[,] game = new Object[8, 8];
    // Start is called before the first frame update
    void Start()
    {
        InitializeBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeBoard() {
        game[0, 0] = Instantiate(rookw, new Vector3(-3.5f, 0, -3.5f), Quaternion.identity);
        game[0, 7] = Instantiate(rookw, new Vector3(3.5f, 0, -3.5f), Quaternion.identity);
        game[0, 1] = Instantiate(knightw, new Vector3(-2.5f, 0, -3.5f), Quaternion.identity);
        game[0, 6] = Instantiate(knightw, new Vector3(2.5f, 0, -3.5f), Quaternion.identity);
        game[0, 2] = Instantiate(bishopw, new Vector3(-1.5f, 0, -3.5f), Quaternion.identity);
        game[0, 5] = Instantiate(bishopw, new Vector3(1.5f, 0, -3.5f), Quaternion.identity);
        game[0, 3] = Instantiate(queenw, new Vector3(-.5f, 0, -3.5f), Quaternion.identity);
        game[0, 4] = Instantiate(kingw, new Vector3(.5f, 0, -3.5f), Quaternion.identity);
        for (int x = 0; x < 8; x++) {
            game[1, x] = Instantiate(pawnw, new Vector3(-3.5f + x, 0, -2.5f), Quaternion.identity);
        }

        game[7, 0] = Instantiate(rookb, new Vector3(-3.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 7] = Instantiate(rookb, new Vector3(3.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 1] = Instantiate(knightb, new Vector3(-2.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 6] = Instantiate(knightb, new Vector3(2.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 2] = Instantiate(bishopb, new Vector3(-1.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 5] = Instantiate(bishopb, new Vector3(1.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 3] = Instantiate(queenb, new Vector3(-.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 4] = Instantiate(kingb, new Vector3(.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        for (int x = 0; x < 8; x++) {
            game[6, x] = Instantiate(pawnb, new Vector3(-3.5f + x, 0, 2.5f), Quaternion.Euler(0, 180, 0));
        }
        turn = 1;
    }
}
