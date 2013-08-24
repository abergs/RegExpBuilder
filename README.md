RegExpBuilder
=============

A library for building RegExpPattrerns
## What is it?
A linq extensions style of building RegExpPatterns in .NET.

###How to use it

    var builder = new Builder.RegExpBuilder();
	    var r = builder
				.StartOfLine()
                .Digit()
                .EndOfLine()
                .ToRegExp();

        r.Match("1").Success; // true
		r.Match("11").Success); // false

There is alot more examples in the test files!
