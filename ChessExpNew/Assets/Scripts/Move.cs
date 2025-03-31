using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move // basic class to examine moves
{
    public int startz;
    public int startx;
    public int endz;
    public int endx;
    public int MVVLVA;
    public bool castleleft = false;
    public bool castleright = false;
    public int castlez = 0;
    public bool en_passant = false;
    public bool passant_flag = false;
    public int promotion = 0;

    public Move(int a, int b, int c, int d, int e, int f) {
        startz = a;
        startx = b;
        endz = c;
        endx = d;
        MVVLVA = f - e;
    }

    public Move(int a, int b, int c, int d, int e, int f, bool en_pass) {
        startz = a;
        startx = b;
        endz = c;
        endx = d;
        MVVLVA = f - e;
        en_passant = en_pass;
    }

    public Move(int a, int b, int c, int d, int e, int f, int promote) {
        startz = a;
        startx = b;
        endz = c;
        endx = d;
        MVVLVA = f - e;
        promotion = promote;
    }
    
    public void castleLeft(int z) {
        castlez = z;
        castleleft = true;
    }
    public void castleRight(int z) {
        castlez = z;
        castleright = true;
    }
}
