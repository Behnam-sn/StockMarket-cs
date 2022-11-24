using StockMarket.Domain;

namespace StockMarket.Tests
{
    public class StockMarketTests
    {
        [Fact]
        public void EnqueueOrderShouldProcessSellOrderWhenBuyOrderIsAlreadyEnqueuedTest()
        {
            // Arrange
            var sut = new StockMarketProcessor();
            // Act
            // Assert
        }
    }
}