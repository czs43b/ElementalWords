using ElementalWords.Services;

namespace ElementalWords.Tests;

public class ElementWordServiceTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public void ElementalForms_WhenNullOrWhiteSpaceInput_ReturnsEmpty(string? input)
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms(input!);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Theory]
    [InlineData("snack", "Nitrogen (N)")]
    [InlineData("snack", "Actinium (Ac)")]
    [InlineData("snack", "Sulfur (S)")]
    [InlineData("snack", "Potassium (K)")]
    [InlineData("snack", "Carbon (C)")]
    [InlineData("snack", "Tin (Sn)")]
    [InlineData("snack", "Sodium (Na)")]
    public void ElementalForms_ShouldContainExpectedElements(string input, string expectedElement)
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms(input);

        // Assert
        result
            .SelectMany(e => e)
            .Should()
            .Contain(expectedElement);
    }

    [Theory]
    [InlineData("CasPer", "Arsenic (As)")]
    [InlineData("cAsper", "Carbon (C)")]
    [InlineData("CASPER", "Phosphorus (P)")]
    [InlineData("casper", "Erbium (Er)")]
    [InlineData("CaspeR", "Sulfur (S)")]
    [InlineData("cASper", "Calcium (Ca)")]
    [InlineData("snack", "Nitrogen (N)")]
    [InlineData("snack", "Actinium (Ac)")]
    [InlineData("snack", "Sulfur (S)")]
    [InlineData("snack", "Potassium (K)")]
    [InlineData("snack", "Carbon (C)")]
    [InlineData("snack", "Tin (Sn)")]
    [InlineData("snack", "Sodium (Na)")]
    public void ElementalForms_ShouldContainExpectedElementsRegardlessOfCase(string input, string expectedElement)
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms(input);

        // Assert
        result
            .SelectMany(e => e)
            .Should()
            .Contain(expectedElement);
    }

    [Fact]
    public void ElementalForms_WhenNoFoundElements_ReturnsEmpty()
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms("zzz");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ElementalForms_WhenCaseInsensitiveInputs_AreHandledCorrectly()
    {
        // Arrange & Act
        var lower = ElementWordService.ElementalForms("sn");
        var upper = ElementWordService.ElementalForms("SN");
        var lowerNorm = lower.Select(a => string.Join("|", a)).OrderBy(s => s).ToArray();
        var upperNorm = upper.Select(a => string.Join("|", a)).OrderBy(s => s).ToArray();

        // Assert
        Assert.Equal(lowerNorm, upperNorm);
    }

    [Fact]
    public void ElementalForms_ReturnTheCorrectPairs_WhenThereAreTwoValidPathsForSn()
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms("sn")
                     .Select(arr => string.Join(", ", arr))
                     .ToArray();

        // Assert
        Assert.Contains("Sulfur (S), Nitrogen (N)", result);
        Assert.Contains("Tin (Sn)", result);
        Assert.Equal(2, result.Length);
    }

    [Fact]
    public void ElementalForms_ExactExpectedOutput_ForSnack()
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms("snack");

        var expected = new[]
        {
            new[] { "Sulfur (S)", "Nitrogen (N)", "Actinium (Ac)", "Potassium (K)" },
            new[] { "Sulfur (S)", "Sodium (Na)", "Carbon (C)", "Potassium (K)" },
            new[] { "Tin (Sn)", "Actinium (Ac)", "Potassium (K)" }
        };

        var normalisedResult = result.Select(r => string.Join(" | ", r)).OrderBy(s => s).ToArray();
        var normalisedExpected = expected.Select(r => string.Join(" | ", r)).OrderBy(s => s).ToArray();

        // Assert
        Assert.Equal(normalisedExpected, normalisedResult);
    }

    [Fact]
    public void ElementalForms_WhenMaxSymbolLengthIsThree_RespectsMaxLength()
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms("HeNe")
                     .Select(arr => string.Join(", ", arr))
                     .ToArray();

        // Assert
        Assert.Contains("Helium (He), Neon (Ne)", result);
    }

    [Fact]
    public void ElementalForms_WhenInputWithInnerSpaces_ReturnsNoElements()
    {
        // Arrange & Act
        var result = ElementWordService.ElementalForms(" sn ack ");

        // Assert
        Assert.Empty(result);
    }
}
