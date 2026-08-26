using Microsoft.EntityFrameworkCore;
using Serveo.Application.Services;

namespace Serveo.Application.Features
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            // 1. Sanitize đầu vào để tránh lỗi âm/không hợp lệ
            var validPageIndex = pageIndex < 1 ? 1 : pageIndex;
            var validPageSize = pageSize < 1 ? 10 : pageSize;

            // 2. Đếm tổng số lượng bản ghi thỏa điều kiện filter
            var totalCount = await query.CountAsync(cancellationToken);

            // 3. Nếu không có dữ liệu, trả về kết quả rỗng luôn để tiết kiệm 1 query Skip/Take
            if (totalCount == 0)
            {
                return new PagedResult<T>([], 0, validPageIndex, validPageSize);
            }

            // 4. Lấy dữ liệu phân trang
            var items = await query
                .Skip((validPageIndex - 1) * validPageSize)
                .Take(validPageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>(items, totalCount, validPageIndex, validPageSize);
        }
    }
}
