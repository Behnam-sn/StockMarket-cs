namespace StockMarket.Domain
{
    public interface IStockMarketProcessor
    {
        void Open();
        void Close();
        long EnqueueOrder(TradeSide side, decimal price, decimal quantity);
        void Cancel(long orderId);
    }
}