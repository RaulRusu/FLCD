using lab2.DataStructures.HashTable;
using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
            lexicalAnalyzer.Initialize();

            lexicalAnalyzer.Scan(System.IO.File.ReadAllText("program3.txt"));
            lexicalAnalyzer.Log();
        }
    }
}
