using lab2.DataStructures.HashTable;
using lab2.LR0.LR0Tester;
using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
            lexicalAnalyzer.Initialize();

            lexicalAnalyzer.Scan(System.IO.File.ReadAllText("program3.txt"));
            lexicalAnalyzer.Log();*/

            /*var console = new FiniteAutomata.FiniteAutomataConsole();
            console.Run();*/

            var console = new ConsoleLR0Parser();

            console.Run();
        }
    }
}
