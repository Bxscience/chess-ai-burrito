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

    public Move(int a, int b, int c, int d, int e, int f) {
        startz = a;
        startx = b;
        endz = c;
        endx = d;
        MVVLVA = f - e;
    }
}
