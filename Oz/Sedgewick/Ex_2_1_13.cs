// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms.Numerics;
using Oz.Algorithms.Sort.V2;
using System;
using System.Collections.Generic;

namespace Oz.Sedgewick;

/*
 Deck sort. Explain how you would put a deck of cards in order by suit (in the
order spades, hearts, clubs, diamonds) and by rank within each suit, with the restriction
that the cards must be laid out face down in a row, and the only allowed operations are
to check the values of two cards and to exchange two cards (keeping them face down).
 */
public class Ex_2_1_13
{
    private void DeckSort(DeckCard[] cards) =>
        Insertion.Sort(cards);

    public static void TestDeckSort()
    {
        List<DeckCard> cards = new();
        foreach (var suit in Enum.GetValues<Suit>())
        {
            for (var i = 1; i <= 13; i++)
            {
                cards.Add(new DeckCard(suit, i));
            }
        }

        var cardArray = cards.ToArray().Shuffled();
        new Ex_2_1_13().DeckSort(cardArray);

        foreach (var card in cardArray)
        {
            Console.WriteLine(card);
        }
    }
}

public record DeckCard : IComparable<DeckCard>
{
    public DeckCard(Suit suit, int rank) =>
        (Suit, Rank) = (suit, rank);

    public Suit Suit { get; }
    public int Rank { get; }

    public int CompareTo(DeckCard other)
    {
        if ((int)Suit < (int)other.Suit)
        {
            return -1;
        }

        return (int)other.Suit < (int)Suit ? 1 : Rank.CompareTo(other.Rank);
    }
}

public enum Suit
{
    Spades,
    Hearts,
    Clubs,
    Diamonds
}