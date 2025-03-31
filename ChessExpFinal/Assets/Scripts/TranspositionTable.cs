using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Entry
{
    public int depth;
    public int evaluation;
    public Move move;
    public int type;
}

public class TranspositionTable
{
    public Dictionary<ulong, Entry> table = new Dictionary<ulong, Entry>();
    
    public void StoreEntry(ulong hash, int dep, int eval, Move mov, int typ)
    {
        if (table.ContainsKey(hash))
        {
            Entry temp = table[hash];
            if (temp.depth < dep)
            {
                table[hash] = new Entry
                {
                    depth = dep,
                    evaluation = eval,
                    move = mov,
                    type = typ
                };
            }
        }
        else
        {
            table[hash] = new Entry
            {
                depth = dep,
                evaluation = eval,
                move = mov,
                type = typ
            };
        }
    }

    // Retrieve an entry from the transposition table
    public Entry GetEntry(ulong hash, int currentDepth)
    {
        if (table.ContainsKey(hash))
        {
            Entry temp = table[hash];
            if (temp.depth >= currentDepth)
            {
                return temp;
            }
        }
        return null;
    }
}