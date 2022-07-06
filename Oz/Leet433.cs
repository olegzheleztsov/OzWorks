// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

public class Leet433
{
    public int MinMutation(string start, string end, string[] bank)
    {
        const string GeneBase = "ACGT";
        var bankSet = new HashSet<string>(bank);
        var genes = new Queue<string>();
        genes.Enqueue(start);
        bankSet.Remove(start);
        var level = 0;

        while (genes.Count > 0)
        {
            var count = genes.Count;
            for (var i = 0; i < count; i++)
            {
                var current = genes.Dequeue();
                if (current == end)
                {
                    return level;
                }

                foreach (var g in GeneBase)
                {
                    var currentGenes = current.ToCharArray();
                    for (var j = 0; j < currentGenes.Length; j++)
                    {
                        var t = currentGenes[j];
                        currentGenes[j] = g;
                        var next = new string(currentGenes);
                        if (bankSet.Contains(next))
                        {
                            bankSet.Remove(next);
                            genes.Enqueue(next);
                        }

                        currentGenes[j] = t;
                    }
                }
            }

            level++;
        }

        return -1;
    }
}