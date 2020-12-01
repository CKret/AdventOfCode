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

        public static LinkedListNode<T> RemoveGetNext<T>(this LinkedList<T> linkedList, LinkedListNode<T> currentNode)
        {
            var nextNode = currentNode.Next;
            linkedList.Remove(currentNode);
            return nextNode;
        }

        public static LinkedListNode<T> RemoveGetPrevious<T>(this LinkedList<T> linkedList, LinkedListNode<T> currentNode)
        {
            var previousNode = currentNode.Previous;
            linkedList.Remove(currentNode);
            return previousNode;
        }
    }
}
