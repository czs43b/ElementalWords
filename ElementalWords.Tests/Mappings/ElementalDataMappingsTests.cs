using System.Text.RegularExpressions;
using ElementalWords.Data;

namespace ElementalWords.Tests;

public class ElementalDataMappingsTests
{
    [Fact]
    public void Elements_ShouldNotBeNullOrEmpty()
    {
        // Assert
        ElementDataMappings.Elements.Should().NotBeNull();
        ElementDataMappings.Elements.Should().NotBeEmpty();
    }

    [Fact]
    public void Elements_ShouldHaveExactly118Entries()
    {
        // Assert
        ElementDataMappings.Elements.Count.Should().Be(118);
    }

    [Theory]
    [InlineData("H", "Hydrogen")]
    [InlineData("He", "Helium")]
    [InlineData("Na", "Sodium")]
    [InlineData("Sn", "Tin")]
    [InlineData("Uut", "Ununtrium")]   // 113 (pre-2016 temp name)
    [InlineData("Uup", "Ununpentium")] // 115
    [InlineData("Uus", "Ununseptium")] // 117
    [InlineData("Uuo", "Ununoctium")]  // 118
    public void Elements_ShouldContainExpectedKeyValuePairs(string symbol, string name)
    {
        // Assert
        ElementDataMappings.Elements.Should().ContainKey(symbol);
        ElementDataMappings.Elements[symbol].Should().Be(name);
    }

    [Theory]
    [InlineData("h")]
    [InlineData("he")]
    [InlineData("na")]
    [InlineData("sn")]
    [InlineData("uut")]
    public void Elements_ShouldBeCaseSensitive(string lowerCaseKey)
    {
        // Assert
        ElementDataMappings.Elements.ContainsKey(lowerCaseKey).Should().BeFalse();
    }

    [Fact]
    public void Elements_AllKeysShouldBeOfLengthOneToThree()
    {
        // Assert
        ElementDataMappings.Elements.Keys
            .Should()
            .OnlyContain(k => k.Length >= 1 && k.Length <= 3);
    }

    [Fact]
    public void Elements_AllKeysShouldMatchSymbolCasing()
    {
        // Arrange / Act
        var symbolPattern = new Regex(@"^[A-Z][a-z]{0,2}$");

        // Assert
        ElementDataMappings.Elements.Keys
            .Should()
            .OnlyContain(k => symbolPattern.IsMatch(k));
    }

    [Fact]
    public void Elements_NoDuplicateKeysOrEmptyKeys()
    {
        // Arrange / Act
        var keys = ElementDataMappings.Elements.Keys;

        // Assert
        keys.Should().OnlyHaveUniqueItems();
        keys.Should().NotContain(string.Empty);
    }

    [Fact]
    public void Elements_PropertyShouldReturnSameInstanceEachTime()
    {
        // Arrange / Act
        var first = ElementDataMappings.Elements;
        var second = ElementDataMappings.Elements;

        // Assert
        ReferenceEquals(first, second).Should().BeTrue();
    }

    [Fact]
    public void Elements_AllValuesShouldBeNonEmpty()
    {
        // Assert
        ElementDataMappings.Elements.Values
            .Should()
            .OnlyContain(v => !string.IsNullOrWhiteSpace(v));
    }
}