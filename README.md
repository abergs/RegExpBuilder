RegExpBuilder
=============

A library for building RegExpPatterns
## What is it?
A linq extensions style of building RegExpPatterns in .NET.

#How to use it


### Of("Github").Or().Of("BitBucket")		
            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .Exactly(1).Of("github")
                .Or()
                .Exactly(1).Of("bitbucket")
                .EndOfLine()
                .ToRegExp();
			
			// r.ToString() == "(?:^)(github{1,1}|(?:bitbucket{1,1}))(?:$)"

            Assert.IsTrue(r.Match("github").Success, "Found one Github");
            Assert.IsTrue(r.Match("bitbucket").Success, "Found one Bitbucket");

            Assert.IsFalse(r.Match("githubgithub").Success, "Oops, Found too Many Github");
            Assert.IsFalse(r.Match("bitbucketbitbucket").Success, ""Oops, Found too Many Github");
       

### Find one digit
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

###There is alot more examples in the test files!
[RegExpBuilderTests.cs](https://github.com/abergs/RegExpBuilder/blob/master/RegExpBuilderTests/RegExpBuilderTests.cs)