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
        struct QueueItem
        {
            public QueueItem(int data)
            {
                this.Data = data;
                Completion = new TaskCompletionSource<int>();
            }
            public int Data { get; }
            public TaskCompletionSource<int> Completion { get; }
        }
        [Fact]
        public async void AddWithBlcokingProducerTestAsync()
        {
            // Arrange
            var sut = new BlockingCollection<QueueItem>();
            var tasks = new Task[11];
            var j = -1;
            var sum = 0;
            // Act
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    var item = new QueueItem(Interlocked.Increment(ref j));
                    sut.Add(item);

                    if (j == 9) sut.CompleteAdding();

                    var data = await item.Completion.Task;
                    Interlocked.Add(ref sum, data);
                });
            }
            tasks[10] = Task.Run(() =>
            {
                while (!sut.IsAddingCompleted || sut.Count > 0)
                {
                    QueueItem item;
                    if (!sut.TryTake(out item)) continue;
                    item.Completion.SetResult(item.Data + 1);
                }
            });
            await Task.WhenAll(tasks);
            // Assert
            Assert.Equal(55, sum);
        }
    }
}