#region

using System.Collections.Generic;
using System.Text;

#endregion

namespace Oz.LeetCode.Recursion;

public class GenerateParenthesisSolver
{
    public IList<string> GenerateParenthesis(int n)
    {
        IList<string> result = new List<string>();
        Helper(new StringBuilder(), n, n);
        return result;

        void Helper(StringBuilder cur, int openedCount, int closedCount)
        {
            if (closedCount == 0)
            {
                result.Add(cur.ToString());
            }
            else
            {
                if (openedCount == closedCount)
                {
                    cur.Append('(');
                    Helper(cur, openedCount - 1, closedCount);
                    cur.Remove(cur.Length - 1, 1);
                }
                else
                {
                    if (openedCount > 0)
                    {
                        cur.Append('(');
                        Helper(cur, openedCount - 1, closedCount);
                        cur.Remove(cur.Length - 1, 1);
                    }

                    if (closedCount <= 0)
                    {
                        return;
                    }

                    cur.Append(')');
                    Helper(cur, openedCount, closedCount - 1);
                    cur.Remove(cur.Length - 1, 1);
                }
            }
        }
    }
}