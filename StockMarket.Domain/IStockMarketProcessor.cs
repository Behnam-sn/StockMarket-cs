namespace StockMarket.Domain
{
    public interface IStockMarketProcessor
    {
        void Open();
        long EnqueueOrder(TradeSide side, decimal price, decimal quantity);
    }
}