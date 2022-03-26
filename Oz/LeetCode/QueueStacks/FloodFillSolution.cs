namespace Oz.LeetCode.QueueStacks;

public class FloodFillSolution
{
    public int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
    {
        var oldColor = image[sr][sc];

        if (oldColor == newColor)
        {
            return image;
        }

        image[sr][sc] = newColor;
        if (IsIndexValid(image, sr - 1, sc) && IsColorSame(image, sr - 1, sc, oldColor))
        {
            FloodFill(image, sr - 1, sc, newColor);
        }

        if (IsIndexValid(image, sr + 1, sc) && IsColorSame(image, sr + 1, sc, oldColor))
        {
            FloodFill(image, sr + 1, sc, newColor);
        }

        if (IsIndexValid(image, sr, sc - 1) && IsColorSame(image, sr, sc - 1, oldColor))
        {
            FloodFill(image, sr, sc - 1, newColor);
        }

        if (IsIndexValid(image, sr, sc + 1) && IsColorSame(image, sr, sc + 1, oldColor))
        {
            FloodFill(image, sr, sc + 1, newColor);
        }

        return image;
    }

    private static bool IsIndexValid(int[][] image, int r, int c) =>
        r >= 0 && r < image.Length && c >= 0 && c < image[0].Length;

    private static bool IsColorSame(int[][] image, int r, int c, int testColor) =>
        image[r][c] == testColor;
}