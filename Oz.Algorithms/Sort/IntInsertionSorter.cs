namespace Oz.Algorithms.Sort
{
    public class IntInsertionSorter : InsertionSorter<int>
    {
        public void Sort(int[] elements, SortDirection direction = SortDirection.Ascending)
        {
            base.Sort(elements, element => element, direction);
        }
    }
}