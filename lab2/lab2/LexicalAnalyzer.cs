using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab2
{
    public class LexicalAnalyzer
    {
        private List<string> reservedTokens;
        private PIF pif = new PIF();
        private SymbolTable identifiersSymbolTable = new SymbolTable();
        private SymbolTable constantsSymbolTable = new SymbolTable();

        public LexicalAnalyzer()
        {
        }

        public void Initialize()
        {
            var reservedTokenText = System.IO.File.ReadAllText("token.in");

            var splitedTokens = reservedTokenText.Split(" ");
            reservedTokens = splitedTokens.ToList();
        }

        public bool IsReserved(string token)
        {
            foreach(var reservedToken in reservedTokens)
            {
                if (token.Equals(token))
                    return true;
            }

            return false;
        }

        public bool IsIdentifier(string token)
        {
            Regex regex = new Regex(@"^([a-z][A-Z])([a-z][A-Z][0-9])*");
            var match = regex.Match(token);
            return match.Success;
        }

        public bool IsInt(string token)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            var match = regex.Match(token);
            return match.Success;
        }

        public bool IsFloat(string token)
        {
            Regex regex = new Regex(@"^[0-9]+[0-9]+$");
            var match = regex.Match(token);
            return match.Success;
        }

        private List<string> DeepTokenize(string simpleToken)
        {
            var result = new List<string>();
            
            var composeOperators = new List<string>();

            composeOperators.AddRange(simpleToken.Split(new string[] { ">=" }, StringSplitOptions.None));
            composeOperators.AddRange(simpleToken.Split(new string[] { "<=" }, StringSplitOptions.None));

            composeOperators.ForEach(token =>
            {
                result.AddRange(token.Split("+-*/%<>="));
            });

            return result;
        }

        public void Scan(string text)
        {
            var tokens = text.Split("{};() ");

            tokens.ToList().ForEach(token =>
            {
                var tokenParts = DeepTokenize(token);

                tokenParts.ForEach(tokenPart =>
                {
                    if (IsReserved(tokenPart))
                        pif.Add(tokenPart, null);
                    else
                    if (IsIdentifier(tokenPart))
                        pif.Add(tokenPart, identifiersSymbolTable.Position(tokenPart));
                    else
                    {
                        if (IsInt(tokenPart))
                            pif.Add(tokenPart, constantsSymbolTable.Position(tokenPart));
                        if (IsBool(tokenPart))
                            pif.Add(tokenPart, constantsSymbolTable.Position(tokenPart));
                        if (IsChar(tokenPart))
                            pif.Add(tokenPart, constantsSymbolTable.Position(tokenPart));
                        if (IsFloat(tokenPart))
                            pif.Add(tokenPart, constantsSymbolTable.Position(tokenPart));
                    }
                });
            });
        }
    }
}
