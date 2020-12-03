using lab2.DataStructures.HashTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public class SymbolTable
    {
        private HashTableCustom hashTable;
        public int Size => hashTable.Size;

        public SymbolTable()
        {
            hashTable = new HashTableCustom();
        }

        public SymbolTable(int size)
        {
            hashTable = new HashTableCustom(size);
        }

        /// <summary>
        /// Find the element with the given token in the internal representation
        /// If it doesn't exist it will be added
        /// </summary>
        /// <param name="token"></param>
        /// <returns>The position of the token in the interal representation</returns>
        public HashTablePosition Position(string token)
        {
            var position = hashTable.Find(token);

            if (position == null)
                position = hashTable.Add(token);

            return position;
        }

        public override string ToString()
        {
            return hashTable.ToString();
        }
    }
}
