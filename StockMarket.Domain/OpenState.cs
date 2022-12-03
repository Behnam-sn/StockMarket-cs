namespace StockMarket.Domain
{
    internal class OpenState : StockMarketState
    {
        public OpenState(StockMarketProcessor stockMarket) : base(stockMarket)
        {
        }
        public override void Open()
        {
        }
        public override long EnqueueOrder(TradeSide side, decimal price, decimal quantity)
        {
            return stockMarket.enqueueOrder(side, price, quantity);
        }
    }
}