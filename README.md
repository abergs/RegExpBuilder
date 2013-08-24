RegExpBuilder
=============

A library for building RegExpPatterns
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


### Exactly().Of("yourString")
	public void ExactlyOfCustom()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .Exactly(3)
                .Of("a")
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("aaa").Success, "Three Letters");
            Assert.IsFalse(r.Match("aaaa").Success, "four Letters");
            Assert.IsFalse(r.Match("aa").Success, "two Letters");
        }

There is alot more examples in the test files!
[RegExpBuilderTests.cs](https://github.com/abergs/RegExpBuilder/blob/master/RegExpBuilderTests/RegExpBuilderTests.cs)