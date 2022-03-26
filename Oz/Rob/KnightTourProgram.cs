#region

using static System.Console;

#endregion

namespace Oz.Rob;

public static class KnightTourProgram
{
    public static void Run()
    {
        var done = false;
        while (!done)
        {
            Write("Enter field size (for exit q): ");
            var input = ReadLine();
            if (input.ToLower().Trim() == "q")
            {
                done = true;
                continue;
            }

            if (int.TryParse(input, out var size))
            {
                var solver = new KnightTourSolver(size, size);
                var result = solver.SolveWansdorff();
                WriteLine($"Success: {result.Success}, Elapsed: {result.Elapsed.TotalSeconds}");
            }
            else
            {
                WriteLine("Wrong size");
            }
        }
    }
}