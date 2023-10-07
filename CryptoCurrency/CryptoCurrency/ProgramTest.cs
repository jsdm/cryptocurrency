using Xunit;

public class ProgramTest {
    [Fact]
    public void EnterCurrencyCorrectlyIsOk()
    {
        // Given
        string newCurrencyName = "SEK";
        double newCurrencyValueInDollars = 0.6417;

        Converter.KryptoValuta converter = new();
        // When
        converter.SetPricePerUnit(newCurrencyName, newCurrencyValueInDollars);
        // Then
        Assert.Equal(newCurrencyValueInDollars, converter.currencyList[newCurrencyName]);
    }
    [Theory]
    [InlineData("",2)]
    [InlineData("TEST",-2)]
    [InlineData("",-3)]
    public void EnterCurrencyWrongThrowsException(string currencyName, double currencyValue)
    {
        // Given
        Converter converter = new();
        // Then
        Assert.Throws<ArgumentException>(() => converter.SetPricePerUnit(currencyName, currencyValue));
    }
    [Theory]
    [InlineData("DOGE",0.06159,"ADA",0.2643)]
    [InlineData("USDT",1,"XRP",0.5247)]
    public void ConvertFromOneToAnotherIsCorrect(string currencyOne,
    double valueCurrencyOne,string currencyTwo, double valueCurrencyTwo)
    {
        // Arrange
        Converter converter = new();
        // Act
        converter.SetPricePerUnit(currencyOne, valueCurrencyOne);
        converter.SetPricePerUnit(currencyTwo, valueCurrencyTwo);
        double convertedValue = converter.Convert(currencyOne, currencyTwo, 100);
        double expected = valueCurrencyTwo * 100 / valueCurrencyOne;
        // Assess
        Assert.Equal(expected,convertedValue);
    }
}
