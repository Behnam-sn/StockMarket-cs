using System.Collections.Concurrent;

namespace StockMarket.Tests
{
    public class BlockingCollectionTests
    {
        [Fact]
        public async Task AddTestAsync()
        {
            // Arrange
            var sut = new BlockingCollection<int>();
            // Act
            var task1 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    sut.Add(i);
                    //await Task.Delay(100);
                }
                sut.CompleteAdding();
            });

            var sum = 0;
            var task2 = Task.Run(() =>
            {
                while (!sut.IsAddingCompleted || sut.Count > 0)
                {
                    int item;
                    if (!sut.TryTake(out item)) continue;
                    sum += item;
                }
            });

            await Task.WhenAll(task1, task2);
            // Assert
            Assert.Equal(45, sum);
        }
    }
}