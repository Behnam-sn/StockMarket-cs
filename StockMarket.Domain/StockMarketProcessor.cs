namespace StockMarket.Domain
{
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