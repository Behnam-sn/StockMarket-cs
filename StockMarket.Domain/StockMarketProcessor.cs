namespace StockMarket.Domain
{
    public enum MarketState
    {
        Close,
        PreOpening,
        Open
    }
    public class StockMarketProcessor
    {
        private MarketState state;
        public StockMarketProcessor()
        {
        }

        public void Open()
        {
            state = MarketState.Open;
        }
    }
}