using FluentAssertions;
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
            var buyOrderId = sut.EnqueueOrder(TradeSide.Buy, 1500M, 1M);
            var sellOrderId = sut.EnqueueOrder(TradeSide.Sell, 1400M, 2M);
            // Assert
            Assert.Equal(1, sut.Trades.Count());
            sut.Trades.First().Should().BeEquivalentTo(new
            {
                BuyOrderId = buyOrderId,
                SellOrderId = sellOrderId,
                Price = 1400M,
                Quantity = 1M
            });
        }
    }
}