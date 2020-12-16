using System;
using System.Linq;
using System.Threading.Tasks;
using Oz.Algorithms;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.DataStructures.Trees;
using Oz.Algorithms.Numerics;

namespace Oz
{
    public class SearchBinaryTreeCase
    {
        public async Task RunAsync()
        {
            var randomArray = new ShuffledArray<int>(Enumerable.Range(1, 10).ToArray());
            var tree = new SearchBinaryTree<int>(val => val);

            Console.WriteLine("Inserting:");
            foreach (var element in randomArray)
            {
                Console.Write($"{element}, ");
                await tree.InsertAsync(element);
            }

            Console.WriteLine(tree.ToString());

            Console.WriteLine("Deleting:");
            var elem5 = await tree.SearchAsync(5);
            await tree.DeleteAsync(elem5);
            Console.WriteLine(tree.ToString());

            Console.WriteLine("Random insert:");
            var randomSource = new DefaultRandomSource();
            for (int i = 0; i < 10; i++)
            {
                await tree.InsertAsync(randomSource.RandomValue(1, 100)).ConfigureAwait(false);
            }
            Console.WriteLine(tree.ToString());
        }

        public async Task InorderPreorderPostorderVisitTests()
        {
            var tree = new SearchBinaryTree<int>(key => key);

            foreach (var element in new ShuffledArray<int>(Enumerable.Range(1, 10).ToArray()))
            {
                await tree.InsertAsync(element).ConfigureAwait(false);
            }

            Console.WriteLine("Inorder:");
            await tree.InorderTreeWalkAsync(node => Console.Write($"{node.Data}, ")).ConfigureAwait(false);
            Console.WriteLine();
            Console.WriteLine("Preorder:");
            await tree.PreorderTreeWalkAsync(node => Console.Write($"{node.Data}, ")).ConfigureAwait(false);
            Console.WriteLine();
            Console.WriteLine("Postorder:");
            await tree.PostorderTreeWalkAsync(node => Console.Write($"{node.Data}, ")).ConfigureAwait(false);
            Console.WriteLine();
        }

        public void MinimumMaximum()
        {
            var tree = new SearchBinaryTree<int>(key => key);

            foreach (var element in new ShuffledArray<int>(Enumerable.Range(1, 10).ToArray()))
            {
                tree.Insert(element);
            }
            
            Console.WriteLine($"Min: {tree.Minimum().Data}, Min Recur: {tree.Minimum(SearchMethod.Recursive).Data}");
            Console.WriteLine($"Max: {tree.Maximum().Data}, Max Recur: {tree.Maximum(SearchMethod.Recursive).Data}");
        }

        public void SuccessorPredecessor()
        {
            var tree = new SearchBinaryTree<int>(key => key);

            foreach (var element in new ShuffledArray<int>(Enumerable.Range(1, 10).ToArray()))
            {
                tree.Insert(element);
            }

            var n5 = tree.Search(5);
            var pred = tree.Predecessor(n5);
            var succ = tree.Successor(n5);
            Console.WriteLine($"Pred: {pred.Data}, Succ: {succ.Data}");
        }
    }
}