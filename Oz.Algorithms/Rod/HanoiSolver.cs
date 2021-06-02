using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oz.Algorithms.Rod
{
    public class HanoiSolver
    {
        private readonly int _discCount;
        private readonly Peg _pegA = new("A");
        private readonly Peg _pegB = new("B");
        private readonly Peg _pegC = new("C");

        public HanoiSolver(int discCount)
        {
            _discCount = discCount;
        }

        public void Run()
        {
            Solve(_pegA, _pegB, _pegC, _discCount, (p1, p2) =>
            {
                Console.Write($"{p1.Name} => {p2.Name} ");
            });
        }

        private void Solve(Peg start, Peg middle, Peg end, int discCount, Action<Peg,Peg> moveAction)
        {
            if (discCount > 1)
            {
                Solve(start, end, middle, discCount - 1, moveAction);
            }

            moveAction(start, end);

            if (discCount > 1)
            {
                Solve(middle, start, end, discCount - 1, moveAction);
            }
        }

        public async Task SolveAsync(Pillar start, Pillar middle, Pillar end, int discCount,
            Func<Pillar, Pillar, Task> moveAction, CancellationToken cancellationToken)
        {
            
            cancellationToken.ThrowIfCancellationRequested();
            if (discCount > 1)
            {
                await SolveAsync(start, end, middle, discCount - 1, moveAction, cancellationToken).ConfigureAwait(false);
            }

            cancellationToken.ThrowIfCancellationRequested();
            await moveAction(start, end).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();
            if (discCount > 1)
            {
                await SolveAsync(middle, start, end, discCount - 1, moveAction, cancellationToken);
            }
        }
        
        
        private record Peg(string Name);

        public record Pillar(int Index);
    }
    
}