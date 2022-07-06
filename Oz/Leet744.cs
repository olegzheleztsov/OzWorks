// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet744
{
    public char NextGreatestLetter(char[] letters, char target)
    {
        if (target >= letters[^1])
        {
            return letters[0];
        }

        var minIndex = 0;
        var maxIndex = letters.Length - 1;
        while (minIndex < maxIndex)
        {
            var mid = minIndex + ((maxIndex - minIndex) / 2);
            if (letters[mid] == target)
            {
                var sn = mid + 1;
                while (letters[sn] == letters[mid])
                {
                    sn++;
                }

                return letters[sn];
            }

            if (letters[mid] < target)
            {
                minIndex = mid + 1;
            }
            else
            {
                maxIndex = mid - 1;
            }
        }

        while (letters[minIndex] <= target)
        {
            minIndex++;
        }

        return letters[minIndex];
    }
}