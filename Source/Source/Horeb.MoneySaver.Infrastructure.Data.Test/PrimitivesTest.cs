using FluentAssertions;
using FluentAssertions.Execution;
using System;
using Xunit;

namespace Horeb.MoneySaver.Infrastructure.Data.Test
{
    public class PrimitivesTest
    {
        [Fact]
        public void DateTimeTest()
        {
            DateTime january = new DateTime(2022, 01, 30);
            DateTime february = january.AddMonths(1);
            using (new AssertionScope())
            {
                february.Month.Should().Be(2);
                february.Day.Should().Be(28);
            }                
        }
    }
}