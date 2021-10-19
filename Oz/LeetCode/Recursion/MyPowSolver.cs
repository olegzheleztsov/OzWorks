namespace Oz.LeetCode.Recursion;

public class MyPowSolver
{
    public double MyPow(double x, int n)
    {
        switch (n)
        {
            case < 0:
                return 1 / x * MyPow(1 / x, -(n + 1));
            case 0:
                return 1;
            case 1:
                return x;
        }

        if (n % 2 == 0)
        {
            return MyPow(x * x, n / 2);
        }

        return x * MyPow(x * x, n / 2);
    }
}