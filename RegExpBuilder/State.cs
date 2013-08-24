using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegExpBuilder
{
    class State
    {
        public State()
        {
            Options = new RegexOptions();
        }

        private bool _some;

        public bool Some
        {
            get
            {
                var v = _some;
                _some = false;
                return v;
            }
            set { _some = value; }
        }


        private bool _zeroOrMore;

        public bool ZeroOrOne
        {
            get
            {
                var v = _zeroOrMore;
                _zeroOrMore = false;
                return v;
            }
            set { _zeroOrMore = value; }
        }


        private int _minimumOf = -1;

        public int MinimumOf
        {
            get
            {
                var v = _minimumOf;
                _minimumOf = -1;
                return v;
            }
            set { _minimumOf = value; }
        }


        private int _maximumOf = -1;
        public bool MultiLine;

        public int MaximumOf
        {
            get
            {
                var v = _maximumOf;
                _maximumOf = -1;
                return v;
            }
            set { _maximumOf = value; }
        }


        public RegexOptions Options { get; set; }
    }
}
