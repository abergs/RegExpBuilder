using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegExpBuilderTests
{
    [TestClass]
    public class RegExpBuilderTests
    {
        [TestMethod]
        public void GetRegExp()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder.Digit().ToRegExp();
            
            Assert.IsTrue(r.Match("1").Success);
            Assert.IsFalse(r.Match("a").Success);
        }

        [TestMethod]
        public void IsDigit() {

            var builder = new Builder.RegExpBuilder();
            var r = builder.Digit().ToRegExp();

            Assert.IsTrue(r.Match("1").Success);
            Assert.IsTrue(r.Match("11").Success);
            Assert.IsFalse(r.Match("a").Success);
        }

        [TestMethod]
        public void IsOnlyOneDigit()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .Digit()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("1").Success);
            Assert.IsFalse(r.Match("11").Success);
            Assert.IsFalse(r.Match("a").Success);
        }

        [TestMethod]
        public void IsSomeDigit()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .OneOrMore()
                .Digit()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("11").Success);
            Assert.IsFalse(r.Match("a").Success);
        }

        [TestMethod]
        public void IsLetters()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .Digit()
                .Letters()
                .Digit()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("1a1").Success);
            Assert.IsTrue(r.Match("1aa1").Success);
            Assert.IsFalse(r.Match("a").Success);
        }

        [TestMethod]
        public void IsLetter()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .Letter()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("a").Success);
            Assert.IsFalse(r.Match("aa").Success);
        }

        [TestMethod]
        public void ZeroOrOneLetter()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .ZeroOrOne()
                .Letter()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("").Success,"None");
            Assert.IsTrue(r.Match("a").Success,"One");
            Assert.IsFalse(r.Match("aa").Success,"Multiple");
        }

        [TestMethod]
        public void Min3Letter()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .MinimumOf(3)
                .Letters()
                .ToRegExp();

            Assert.IsTrue(r.Match("aaa").Success, "Three Letters");
            Assert.IsTrue(r.Match("bbbb").Success, "Four Letters");
            Assert.IsFalse(r.Match("aa").Success, "Two Letters");
        }

        [TestMethod]
        public void Max3Letter()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .MaximumOf(3)
                .Letters()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("aaa").Success, "Three Letters");
            Assert.IsTrue(r.Match("aa").Success, "Two Letters");
            Assert.IsFalse(r.Match("bbbb").Success, "Four Letters");
        }

        [TestMethod]
        public void Min3Max4Letter()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .MinimumOf(3)
                .MaximumOf(4)
                .Letters()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("aaa").Success, "Three Letters");
            Assert.IsTrue(r.Match("aaaa").Success, "Four Letters");
            Assert.IsFalse(r.Match("bbbbb").Success, "Five Letters");
            Assert.IsFalse(r.Match("bb").Success, "Two Letters");
        }

        [TestMethod]
        public void Exactly()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .Exactly(3)
                .Letters()
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("aaa").Success, "Three Letters");
            Assert.IsFalse(r.Match("aaaa").Success, "four Letters");
            Assert.IsFalse(r.Match("bb").Success, "two Letters");
        }

        [TestMethod]
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

        [TestMethod]
        public void Or()
        {

            var builder = new Builder.RegExpBuilder();
            var r = builder
                .StartOfLine()
                .Exactly(1)
                .Of("a")
                .Or()
                .Exactly(1).Of("b")
                .EndOfLine()
                .ToRegExp();

            Assert.IsTrue(r.Match("a").Success, "a");
            Assert.IsTrue(r.Match("b").Success, "b");
            Assert.IsFalse(r.Match("ab").Success, "two Letters");
            Assert.IsFalse(r.Match("aa").Success, "two Letters");
        }
    }
}
