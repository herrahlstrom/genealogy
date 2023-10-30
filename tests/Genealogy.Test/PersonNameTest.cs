using FluentAssertions;
using Genealogy.Shared;

namespace Genealogy.Test;

[TestClass]
public class PersonNameTest
{
    [TestMethod]
    [DataRow("Anna Maria /Andersson")]
    [DataRow("Anna Maria /Andersson//")]
    public void InvalidSingleNames_ShouldThrow(string input)
    {
        Action action = () => _ = new PersonName(input);

        action.Should().Throw<ArgumentException>();
    }
    
    [TestMethod]
    public void Nickname_ShouldParse()
    {
        string input = "Anna Maria Katarina* \"Kata\" /Carlberg/";
        var expected = new Expected(FirstName: "Anna Maria Katarina* \"Kata\"", GivenName: "Katarina", LastName: "Carlberg", FullName: "Anna Maria Katarina* \"Kata\" /Carlberg/", NickName:"Kata", DisplayName: "Kata Carlberg");

        var name = new PersonName(input);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void PairBasic_ShouldParse()
    {
        string fn = "Anna";
        string ln = "Andersson";
        var expected = new Expected(FirstName: "Anna", GivenName: "Anna", LastName: "Andersson", FullName: "Anna /Andersson/", DisplayName: "Anna Andersson");

        var name = new PersonName(fn, ln);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void PairFullName_ImplicitLastName_ShouldParse()
    {
        string fn = "Anna Maria*";
        string ln = "Andersson";
        var expected = new Expected(FirstName: "Anna Maria*", GivenName: "Maria", LastName: "Andersson", FullName: "Anna Maria* /Andersson/", DisplayName: "Maria Andersson");

        var name = new PersonName(fn, ln);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void PairGivenNameNotFirst_ShouldParse()
    {
        string fn = "Anna Maria* Britta";
        string ln = "Andersson";
        var expected = new Expected(FirstName: "Anna Maria* Britta", GivenName: "Maria", LastName: "Andersson", FullName: "Anna Maria* Britta /Andersson/", DisplayName: "Maria Andersson");

        var name = new PersonName(fn, ln);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void PairMultipleFirstNames_ShouldParse()
    {
        string fn = "Anna Maria";
        string ln = "Andersson";
        var expected = new Expected(FirstName: "Anna Maria", GivenName: "Anna", LastName: "Andersson", FullName: "Anna Maria /Andersson/", DisplayName: "Anna Andersson");

        var name = new PersonName(fn, ln);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void SingleBasic_ShouldParse()
    {
        string input = "Anna /Andersson/";
        var expected = new Expected(FirstName: "Anna", GivenName: "Anna", LastName: "Andersson", FullName: "Anna /Andersson/", DisplayName: "Anna Andersson");

        var name = new PersonName(input);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void SingleFullName_ImplicitLastName_ShouldParse()
    {
        string input = "Anna Maria* Andersson";
        var expected = new Expected(FirstName: "Anna Maria*", GivenName: "Maria", LastName: "Andersson", FullName: "Anna Maria* /Andersson/", DisplayName: "Maria Andersson");

        var name = new PersonName(input);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void SingleGivenNameNotFirst_ShouldParse()
    {
        string input = "Anna Maria* Britta /Andersson/";
        var expected = new Expected(FirstName: "Anna Maria* Britta", GivenName: "Maria", LastName: "Andersson", FullName: "Anna Maria* Britta /Andersson/", DisplayName: "Maria Andersson");

        var name = new PersonName(input);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void SingleMultipleFirstNames_ShouldParse()
    {
        string input = "Anna Maria /Andersson/";
        var expected = new Expected(FirstName: "Anna Maria", GivenName: "Anna", LastName: "Andersson", FullName: "Anna Maria /Andersson/", DisplayName: "Anna Andersson");

        var name = new PersonName(input);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void SingleName_ShouldParseAsFirstName()
    {
        string input = "Anna";
        var expected = new Expected(FirstName: "Anna", GivenName: "Anna", LastName: "", FullName: "Anna //", DisplayName: "Anna");

        var name = new PersonName(input);
        name.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void SingleName_ShouldParseAsLastName()
    {
        string input = "/Anna/";
        var expected = new Expected(FirstName: "", GivenName: "", LastName: "Anna", FullName: "/Anna/", DisplayName: "Anna");

        var name = new PersonName(input);
        name.Should().BeEquivalentTo(expected);
    }

    private record Expected(string FirstName, string GivenName, string LastName, string FullName, string DisplayName, string NickName = "");
}
