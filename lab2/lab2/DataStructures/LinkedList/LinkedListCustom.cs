using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.DataStructures.LinkedList
{
    public class LinkedListCustom
    {
        public LinkedListNodeCustom First { get; set; } = null;
        public LinkedListNodeCustom Last { get; set; } = null;
        public int Size { get; private set; } = 0;

        public LinkedListNodeCustom AddLast(string value)
        {
            var node = new LinkedListNodeCustom(value);

            if (First == null)
            {
                this.First = node;
                this.Last = node;
            }
            else
            {
                this.Last.Next = node;
                this.Last = node;
            }

            Size++;
            return node;
        }

        public int? GetIndex(string value)
        {
            var currentNode = this.First;
            var index = 0;

            while(currentNode != null)
            {
                if (currentNode.Value == value)
                    return index;

                index++;
                currentNode = currentNode.Next;
            }

            return null;
        }

        public override string ToString()
        {
            var str = "";
            var currentNode = this.First;

            while (currentNode != null)
            {
                str += currentNode.Value.ToString() + " ";

                currentNode = currentNode.Next;
            }

            return str;
        }
    }
}
