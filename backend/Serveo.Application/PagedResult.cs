namespace Serveo.Application.Services
{
    public class PagedResult<T>(IReadOnlyList<T> items, int itemCount, int pageIndex, int pageSize)
    {
        public IReadOnlyList<T> Items { get; } = items ?? [];
        public int ItemCount { get; } = itemCount;
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;

        // Các thuộc tính tiện ích bổ sung cho Client
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(ItemCount / (double)PageSize) : 0;
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }

    public class CursorPagedResult<T>(
    IReadOnlyList<T> items,
    string? nextCursor,
    int pageSize,
    string? previousCursor = null)
    {
        public IReadOnlyList<T> Items { get; } = items ?? [];
        public string? NextCursor { get; } = nextCursor;
        public string? PreviousCursor { get; } = previousCursor;
        public int PageSize { get; } = pageSize;
        public int Count => Items.Count;
        public bool HasNextPage => !string.IsNullOrWhiteSpace(NextCursor);
        public bool HasPreviousPage => !string.IsNullOrWhiteSpace(PreviousCursor);
    }
}
