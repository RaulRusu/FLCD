using lab2.DataStructures.HashTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public class PIF
    {
        private List<KeyValuePair<string, HashTablePosition>> tokenDictionary = new List<KeyValuePair<string, HashTablePosition>>();

        public void Add(string token, HashTablePosition postion)
        {
            tokenDictionary.Add(new KeyValuePair<string, HashTablePosition>(token, postion));
        }

        public override string ToString()
        {
            var str = "";

            tokenDictionary.ForEach(pair =>
            {
                if (pair.Value == null)
                    str += pair.Key + " " + "null" + "\n";
                else
                    str += pair.Key + " " + pair.Value.ToString() + "\n";
            });

            return str;
        }
    }
}
