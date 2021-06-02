using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Oz.LeetCode.Recursion
{
    public class SwapPairsSolution
    {
        public ListNode SwapPairs(ListNode head) {
            if (head == null)
            {
                return null;
            }

            if (head.next == null)
            {
                return head;
            }

            var after2Node = head.next.next;
            var temp = head;
            head = head.next;
            head.next = temp;
            temp.next = SwapPairs(after2Node);
            return head;
        }
    }
}