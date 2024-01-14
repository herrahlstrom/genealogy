using FluentAssertions;
using Genealogy.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Genealogy.Test;

[TestClass]
public class DateModelTest
{
    [TestMethod]
    public void FullDate_ShouldParseFullDate()
    {
        string input = "1984-09-05";
        int expectedYear = 1984;
        DateOnly expectedDate = new DateOnly(1984, 9, 5);

        var date = new DateModel(input);
        date.Year.Should().Be(expectedYear);
        date.Date.Should().Be(expectedDate);
    }

    [TestMethod]
    [DataRow("1984-09-", 1984)]
    [DataRow("1984--", 1984)]
    [DataRow("1984", 1984)]
    public void PartialDate_ShouldParseYear(string input, int expectedYear)
    {
        var date = new DateModel(input);
        date.Year.Should().Be(expectedYear);
    }

    [TestMethod]
    public void FullDate_ShouldGiveCorrectDisplayDate()
    {
        string input = "1984-09-05";
        string expectedDisplayDate = "5 september 1984";

        var date = new DateModel(input);
        date.GetDisplayDate().Should().BeEquivalentTo(expectedDisplayDate);
    }

    [TestMethod]
    [DataRow("1984-09-", "1984")]
    [DataRow("1984--", "1984")]
    [DataRow("1984", "1984")]
    public void PartialDate_ShouldParseYear(string input, string expectedDisplayDate)
    {
        var date = new DateModel(input);
        date.GetDisplayDate().Should().Be(expectedDisplayDate);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("5 april")]
    public void InvalidDate_ShouldNotParse(string input)
    {
        var date = new DateModel(input);
        
        date.Date.Should().BeNull();
        date.Year.Should().BeNull();

        // Invalid dates should always return original value as display date
        date.GetDisplayDate().Should().Be(input);
    }
}
