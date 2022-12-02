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
            sut.Open();
            // Act
            sut.EnqueueOrder(TradeSide.Buy, 1500M, 1M);
            sut.EnqueueOrder(TradeSide.Sell, 1400M, 2M);
            // Assert
        }
    }
}