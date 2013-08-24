using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RegExpBuilder;

namespace Builder
{
    public class RegExpBuilder
    {
        private List<string> _expression;

        private State _state;

        public RegExpBuilder()
        {
            _state = new State();
            _expression = new List<string>();
        }

        public override string ToString()
        {
            return string.Join("", _expression.ToArray());
        }

        public Regex ToRegExp()
        {
            RegexOptions options = new RegexOptions();
            if (_state.MultiLine)
                options = options | RegexOptions.Multiline;

            return new Regex(this.ToString(),options);
        }

        public RegExpBuilder StartOfInput()
        {
            _expression.Add("(?:^)");
            return this;
        }

        public RegExpBuilder EndOfInput()
        {
            _expression.Add("(?:$)");
            return this;
        }

        public RegExpBuilder StartOfLine()
        {
            _state.MultiLine = true;
                StartOfInput();
            return this;
        }

        public RegExpBuilder EndOfLine()
        {
            _state.MultiLine = true;
            EndOfInput();
            return this;
        }

        public RegExpBuilder OneOrMore()
        {
            _state.Some = true;
            return this;
        }

        public RegExpBuilder Digit()
        {
            AddExpression("d");

            return this;
        }

        public RegExpBuilder Digits()
        {
            AddExpression("d+");

            return this;
        }

        private string _AZaz = "A-Za-z";

        public RegExpBuilder ZeroOrOne()
        {
            _state.ZeroOrOne = true;
            return this;
        }

        public RegExpBuilder Letter()
        {
            _state.Some = false;
            AddFrom(_AZaz);

            return this;
        }

        public RegExpBuilder Letters()
        {
            _state.Some = true;
            AddFrom(_AZaz);

            return this;
        }

        public RegExpBuilder MinimumOf(int minimumOccurence)
        {
            _state.MinimumOf = minimumOccurence;
            return this;
        }

        private string GetQuantityLiteral()
        {
            int min = _state.MinimumOf;
            int max = _state.MaximumOf;

            var literal = "";

            if (min > -1 || max > -1)
            {
                string minValue = min > -1 ? min.ToString() : "0";
                string maxValue = max > -1 ? max.ToString() : "";

                literal = string.Format("{{{0},{1}}}", minValue, maxValue);
            }

            return literal;
        }

        private void AddFrom(string p)
        {
            var from = string.Format("[{0}]", p);
            from = AddFilters(from);

            _expression.Add(from);
        }

        private string AddFilters(string literal)
        {

            var quantitySet = GetQuantityLiteral();
            literal += quantitySet;

            if (quantitySet == string.Empty)
            {
                if (_state.ZeroOrOne && !literal.EndsWith("?"))
                    literal += "?";

                if (_state.Some && !literal.EndsWith("+"))
                    literal += "+";
            }

            return literal;
        }


        private void AddExpression(string literal)
        {
            Add(@"\" + literal);
        }

        private void Add(string _literal)
        {
            _literal = AddFilters(_literal);
            _expression.Add(_literal);
        }

        public RegExpBuilder MaximumOf(int maximumOccurences)
        {
            _state.MaximumOf = maximumOccurences;
            return this;
        }

        public RegExpBuilder Exactly(int occurences)
        {
            _state.MinimumOf = occurences;
            _state.MaximumOf = occurences;
            return this;
        }

        public RegExpBuilder Of(string stringToMatch)
        {
            Add(stringToMatch);
            return this;
        }
    }
}
