namespace Oz.LeetCode.Recursion;

public class SwapPairsSolution
{
    public ListNode SwapPairs(ListNode head)
    {
        if (head == null)
        {
            return null;
        }

        if (head.Next == null)
        {
            return head;
        }

        var after2Node = head.Next.Next;
        var temp = head;
        head = head.Next;
        head.Next = temp;
        temp.Next = SwapPairs(after2Node);
        return head;
    }
}