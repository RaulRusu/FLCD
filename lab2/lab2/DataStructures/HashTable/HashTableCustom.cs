using lab2.DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace lab2.DataStructures.HashTable
{
    public class HashTableCustom
    {
        const int DEFAULT_BUCKET_SIZE = 1000;
        private LinkedListCustom[] buckets;
        public int Size { private set; get; }

        public HashTableCustom()
        {
            this.Size = DEFAULT_BUCKET_SIZE;
            buckets = new LinkedListCustom[DEFAULT_BUCKET_SIZE];
        }

        public HashTableCustom(int size)
        {
            this.Size = size;
            buckets = new LinkedListCustom[size];
        }

        /// <summary>
        /// Computes the hash of a string using djb2 Algorithm (simple to implement, works good in practice)
        /// </summary>
        /// <param name="value"></param>
        private int hashFunction(string value)
        {
            var hash = 5381;

            byte[] asciiBytes = Encoding.ASCII.GetBytes(value);
            asciiBytes.ToList().ForEach(asciiByte => hash = (hash * 33 + asciiByte) % Size);

            return hash;
        }

        /// <summary>
        /// Add an element in the hash table
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The position of the elemnt</returns>
        public HashTablePosition Add(string value)
        {
            var hash = hashFunction(value);

            var bucket = buckets[hash];
            if (buckets[hash] == null)
                bucket = new LinkedListCustom();
            bucket.AddLast(value);

            return new HashTablePosition(hash, bucket.Size - 1);
        }

        /// <summary>
        /// Find an element in the hash table by it's value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The position in the hash table or null if it doesn't exists</returns>
        public HashTablePosition Find(string value)
        {
            var hash = hashFunction(value);

            var bucket = buckets[hash];
            if (bucket == null)
                return null;

            var index = bucket.GetIndex(value);
            if (!index.HasValue)
                return null;

            return new HashTablePosition(hash, index.Value);
        }
    }
}
