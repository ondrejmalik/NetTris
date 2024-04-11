using System;
using System.Collections.Generic;
using Tetris.Game.Game.Playfield.Tetrimino;

namespace Tetris.Game.Game.Bag;

public class Bag
{
    private LinkedList<PieceType> bagQueue = new LinkedList<PieceType>();
    private bool[] wasUsed = new bool[7];

    public LinkedList<PieceType> BagQueue
    {
        get
        {
            if (bagQueue.Count == 0)
            {
                FillBag();
            }

            return bagQueue;
        }
        set
        {
            BagQueue = value;
        }
    }

    public void FillBag()
    {
        bagQueue.Clear();
        int[] bag = new int[7];
        for (int i = 0; i < 7; i++)
        {
            bag[i] = i;
        }

        Random r = new Random();
        r.Shuffle(bag);
        foreach (int i in bag)
        {
            bagQueue.AddLast((PieceType)i);
        }
    }

    public PieceType Dequeue()
    {
        if (bagQueue.Count == 0)
        {
            FillBag();
        }

        PieceType result = bagQueue.First.Value;
        bagQueue.RemoveFirst();

        Random r = new Random();
        List<int> unused = new List<int>();
        for (int i = 0; i < wasUsed.Length; i++)
        {
            if (!wasUsed[i])
            {
                unused.Add(i);
            }
        }

        if (unused.Count == 0)
        {
            for (int i = 0; i < wasUsed.Length; i++)
            {
                wasUsed[i] = false;
            }

            unused = new List<int>();
            for (int i = 0; i < wasUsed.Length; i++)
            {
                unused.Add(i);
            }
        }

        int enqueIndex = unused[r.Next(0, unused.Count - 1)];
        bagQueue.AddLast(new LinkedListNode<PieceType>((PieceType)enqueIndex));
        wasUsed[enqueIndex] = true;
        return result;
    }
}
