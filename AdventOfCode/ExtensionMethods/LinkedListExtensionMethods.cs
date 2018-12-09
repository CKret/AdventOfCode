using System.Collections.Generic;

namespace AdventOfCode.ExtensionMethods
{
    public static class LinkedListExtensionMethods
    {
        public static LinkedListNode<T> NthPrevious<T>(this LinkedListNode<T> currentNode, int count, LinkedListNode<T> lastNode)
        {
            for (var i = 0; i < count; i++)
            {
                currentNode = currentNode?.Previous ?? lastNode;
            }

            return currentNode;
        }
    }
}
