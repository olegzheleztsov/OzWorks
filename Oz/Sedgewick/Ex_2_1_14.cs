// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms.DataStructures;
using Oz.Algorithms.Numerics;
using System;
using System.Collections.Generic;

namespace Oz.Sedgewick;

/// <summary>
/// Wrong solution
/// </summary>
public class Ex_2_1_14
{
    public void DequeueSort(DeckCard[] cards)
    {
        OzDequeue<DeckCard> dequeue = new(cards.Length);

        foreach (var card in cards)
        {
            dequeue.EnqueueLeft(card);
        }

        bool swapped = false;
        do
        {
            swapped = false;
            for (int i = 0; i < cards.Length - 1; i++)
            {
                var right = dequeue.DequeueRight();
                var left = dequeue.DequeueRight();
                if (left.CompareTo(right) > 0)
                {
                    dequeue.EnqueueRight(right);
                    dequeue.EnqueueLeft(left);
                    swapped = true;
                }
                else
                {
                    break;
                }
            }

            var last = dequeue.DequeueRight();
            dequeue.EnqueueLeft(last);
        } while (swapped);
    }

    public void TestDequeueSort()
    {
        List<DeckCard> cards = new();
        foreach (var suit in Enum.GetValues<Suit>())
        {
            for (int i = 1; i <= 13; i++)
            {
                cards.Add(new DeckCard(suit, i));
            }
        }

        var cardArray = cards.ToArray().Shuffled();
        DequeueSort(cardArray);

        foreach (var card in cardArray)
        {
            Console.WriteLine(card);
        }
    }
}