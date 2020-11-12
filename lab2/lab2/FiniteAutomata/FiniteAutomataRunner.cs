using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace lab2.FiniteAutomata
{
    public class FiniteAutomataRunner
    {
        private List<string> states = new List<string>();
        private List<string> alphabet = new List<string>();
        private string startingState;
        private List<string> finalSates = new List<string>();
        private Dictionary<string, Dictionary<string, string>> transitions = new Dictionary<string, Dictionary<string, string>>();

        public FiniteAutomataRunner(string filePathFA)
        {
            StreamReader fileReader = new StreamReader(filePathFA);

            var line = "";

            line = fileReader.ReadLine();
            states = line.Split(" ").ToList();

            line = fileReader.ReadLine();
            alphabet = line.Split(" ").ToList();

            startingState = fileReader.ReadLine();

            line = fileReader.ReadLine();
            finalSates = line.Split(" ").ToList();

            while ((line = fileReader.ReadLine()) != null)
            {
                var splittedLine = line.Split(" ");
                var lState = splittedLine[0];
                var t = splittedLine[1];
                var rState = splittedLine[2];

                if (!transitions.ContainsKey(lState))
                    transitions[lState] = new Dictionary<string, string>();

                transitions[lState][t] = rState;
            }
        }

        public string GetStates()
        {
            var str = "";
            states.ForEach(state => str += state + " ");
            return str;
        }

        public string GetAphabet()
        {
            var str = "";
            alphabet.ForEach(a => str += a + " ");
            return str;
        }

        public string GetInitalState() => startingState;

        public string GetFinalStates()
        {
            var str = "";
            finalSates.ForEach(state => str += state + " ");
            return str;
        }

        public string GetTransitions()
        {
            var str = "";
            var lState = "";
            var t = "";
            var rState = "";
            foreach (var entry in transitions)
            {
                lState = entry.Key;

                foreach(var entry2 in entry.Value)
                {
                    t = entry2.Key;
                    rState = entry2.Value;

                    str += String.Format("{0} {1} {2}\n", lState, t, rState);
                }
            }

            return str;
        }

        public bool IsAccepted(string sequence)
        {
            var currentState = startingState;

            foreach(var c in sequence)
            {
                var strC = c.ToString();

                if (!transitions.ContainsKey(currentState))
                    return false;
                if (!transitions[currentState].ContainsKey(strC))
                    return false;
                currentState = transitions[currentState][strC];
            }

            foreach (var state in finalSates)
                if (state.Equals(currentState))
                    return true;

            return false;
        }
    }

}
