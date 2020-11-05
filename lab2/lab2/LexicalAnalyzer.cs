using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace lab2
{
    public class LexicalAnalyzer
    {
        private List<string> reservedTokens;
        private List<string> separators;
        private List<string> operators;
        private List<string> composedOperators;
        private PIF pif = new PIF();
        private SymbolTable identifiersSymbolTable = new SymbolTable();
        private SymbolTable constantsSymbolTable = new SymbolTable();
        private string LastMessage = "";
        public LexicalAnalyzer()
        {
        }

        public void Initialize()
        {
            var reader = new System.IO.StreamReader(@"token2.in");
            var line = "";
            pif = new PIF();
            identifiersSymbolTable = new SymbolTable();
            constantsSymbolTable = new SymbolTable(); 
            LastMessage = "";

            line = reader.ReadLine();
            separators = line.Split(" ").ToList();
            separators.Add(" ");
            separators.Add("\t");

            line = reader.ReadLine();
            composedOperators = line.Split(" ").ToList();

            line = reader.ReadLine();
            operators = line.Split(" ").ToList();

            line = reader.ReadLine();
            reservedTokens = line.Split(" ").ToList();
        }

        public bool IsReserved(string token)
        {
            foreach (var reservedToken in reservedTokens)
            {
                if (token.Equals(reservedToken))
                    return true;
            }

            return false;
        }

        /// regex: ^([a-z]|[A-Z])([a-z]|[A-Z]|[0-9])*$

        public bool IsIdentifier(string token)
        {
            Regex regex = new Regex(@"^([a-z]|[A-Z])([a-z]|[A-Z]|[0-9])*$");
            var match = regex.Match(token);
            return match.Success;
        }

        /// regex: ^-?(0|[1-9]+[0-9]*)$
        public bool IsInt(string token)
        {
            Regex regex = new Regex(@"^-?(0|[1-9]+[0-9]*)$");
            var match = regex.Match(token);
            return match.Success;
        }
        /// regex: ^-?[0-9]+\.[0-9]+$
        public bool IsFloat(string token)
        {
            Regex regex = new Regex(@"^-?[0-9]+\.[0-9]+$");
            var match = regex.Match(token);
            return match.Success;
        }

        public bool IsBool(string token)
        {
            return token.Equals("True") | token.Equals("False");
        }

        // regex: ^'([A-Z]|[a-z]|[0-9])'$
        public bool IsChar(string token)
        {
            Regex regex = new Regex(@"^'([A-Z]|[a-z]|[0-9])'$");
            var match = regex.Match(token);
            return match.Success;
        }

        public List<string> DeepTokenize(string line)
        {
            var tokensAfterSeparators = line
                .Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var tokensAfterComposedOperators = tokensAfterSeparators
                .Select(token => token.Split(composedOperators.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(tokens => tokens)
                .ToList();

            var tokensAfterOperators = tokensAfterComposedOperators
                .Select(token => token.Split(operators.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(tokens => tokens)
                .ToList();

            return tokensAfterOperators;
        }

        public void Scan(string text)
        {
            var lines = text.Split("\r\n");
            var lineIndex = 0;
            var message = "";
            lines.ToList().ForEach(line =>
            {
                
                var tokens = DeepTokenize(line);
                tokens.ForEach(token =>
                {
                    if (IsReserved(token))
                        pif.Add(token, null);
                    else
                    if (IsIdentifier(token))
                        pif.Add("0", identifiersSymbolTable.Position(token));
                    else
                    {
                        if (IsInt(token))
                            pif.Add("1", constantsSymbolTable.Position(token));
                        else
                        if (IsBool(token))
                            pif.Add("1", constantsSymbolTable.Position(token));
                        else
                        if (IsChar(token))
                            pif.Add("1", constantsSymbolTable.Position(token));
                        else
                        if (IsFloat(token))
                            pif.Add("1", constantsSymbolTable.Position(token));
                        else
                        {
                            message += "Lexical error: " + "Line - " + lineIndex.ToString() + ", Token - " + token + "\n";
                        }
                    }
                    
                });
            });

            if (message == "")
                message = "Lexically correct\n";
            LastMessage = message;
        }

        public void Log()
        {
            using var fileStream = File.Open("output.log", FileMode.Create, FileAccess.Write);
            using var writer = new StreamWriter(fileStream);
            writer.WriteLine(LastMessage);

            writer.WriteLine("PIF");
            writer.WriteLine(pif);

            writer.WriteLine("Identifiers Symbol Table");
            writer.WriteLine(identifiersSymbolTable);

            writer.WriteLine("Constants Symbol Table");
            writer.WriteLine(constantsSymbolTable);
        }
    }
}
