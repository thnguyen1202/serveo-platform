namespace Serveo.Application.Services
{
    public class PagedResult<T>(IReadOnlyList<T> items, int itemCount)
    {
        public int ItemCount { get; } = itemCount;
        public IReadOnlyList<T> Items { get; } = items ?? [];
    }
}
