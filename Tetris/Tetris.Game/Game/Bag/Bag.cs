using System;
using System.Collections.Generic;
using Tetris.Game.Game.Tetrimino;

namespace Tetris.Game.Game.Bag;

/// <summary>
/// Represents a 7 bag of pieces that can be dequeued.
/// </summary>
public class Bag
{
    private LinkedList<PieceType> bagQueue = new LinkedList<PieceType>();
    private bool[] wasUsed = new bool[7];

    /// <summary>
    ///  The queue of pieces.
    /// </summary>
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

    /// <summary>
    ///  Fills the bag with 7 pieces.
    /// </summary>
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

    /// <summary>
    ///  returns and removes a piece from the bag.
    /// </summary>
    /// <returns>First piece in BagQueue</returns>
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
