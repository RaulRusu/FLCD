using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.FiniteAutomata
{
    class FiniteAutomataConsole
    {
        private FiniteAutomataRunner automataRunner = new FiniteAutomataRunner("faInteger.txt");

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1 - states");
                Console.WriteLine("2 - alphabet");
                Console.WriteLine("3 - transitions");
                Console.WriteLine("4 - initial state");
                Console.WriteLine("5 - final states");
                Console.WriteLine("6 - accepted by the finite automata");

                var command = Console.ReadLine();

                if (command.Equals("1"))
                    Console.WriteLine(automataRunner.GetStates());
                if (command.Equals("2"))
                    Console.WriteLine(automataRunner.GetAphabet());
                if (command.Equals("3"))
                    Console.WriteLine(automataRunner.GetTransitions());
                if (command.Equals("4"))
                    Console.WriteLine(automataRunner.GetInitalState());
                if (command.Equals("5"))
                    Console.WriteLine(automataRunner.GetFinalStates());
                if (command.Equals("6"))
                {
                    var sequence = Console.ReadLine();
                    Console.WriteLine(automataRunner.IsAccepted(sequence));
                }
            }
        }
    }
}
