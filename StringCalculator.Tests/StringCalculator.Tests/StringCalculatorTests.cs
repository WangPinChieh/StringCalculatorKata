using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace StringCalculatorKata.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [SetUp]
        public void SetUp()
        {
            _stringCalculator = new StringCalculator();
        }

        private StringCalculator _stringCalculator;

        [Test]
        public void Add_InputEmpty_ShouldBe0()
        {
            Assert.AreEqual(0, _stringCalculator.Add("0"));
        }

        [Test]
        public void Add_InputMultipleNumbers_ShouldSumUp()
        {
            Assert.AreEqual(30, _stringCalculator.Add("2,4,6,8,10"));
        }

        [Test]
        public void Add_InputSingleNumber_ShouldBeSame()
        {
            Assert.AreEqual(1, _stringCalculator.Add("1"));
        }

        [Test]
        public void Add_InputTwoNumbers_ShouldSumUp()
        {
            Assert.AreEqual(3, _stringCalculator.Add("1,2"));
        }

        [Test]
        public void Add_InputTwoNumbersWithCustomDelimiter_ShouldSumUp()
        {
            Assert.AreEqual(3, _stringCalculator.Add("//;\n1;2"));
        }

        [Test]
        public void Add_InputTwoNumbersWithDifferentSeparator_ShouldSumUp()
        {
            Assert.AreEqual(6, _stringCalculator.Add("1\n2,3"));
        }
    }

    public class StringCalculator
    {
        private readonly List<char> _separators = new List<char> {',', '\n'};

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) return 0;

            if (numbers.Length == 1) return Convert.ToInt32(numbers);

            var regex = new Regex("^//(\\S{1})\n(.*)");
            var match = regex.Match(numbers);
            if (match.Success)
            {
                _separators.Add(char.Parse(match.Groups[1].Value));
                numbers = match.Groups[2].Value;
            }

            var numberStrings = numbers.Split(_separators.ToArray());

            if (numberStrings.Length > 0) return numberStrings.Sum(Convert.ToInt32);

            return 0;
        }
    }
}