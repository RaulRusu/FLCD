using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.DataStructures.HashTable
{
    public class HashTablePosition
    {
        public int Bucket { get; set; }
        public int Index { get; set; }

        public HashTablePosition(int bucket, int index)
        {
            Bucket = bucket;
            Index = index;
        }

        public override string ToString()
        {
            return Bucket.ToString() + " " + Index.ToString();
        }
    }
}
