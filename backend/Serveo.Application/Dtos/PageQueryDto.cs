using System.ComponentModel.DataAnnotations;

namespace Serveo.Application.Dtos
{
    public class PageQueryDto(int pageIndex = 1, int pageSize = 10)
    {
        public const int MaxPageSize = 10000;
        public const int MaxPageIndex = int.MaxValue;

        [Range(1, MaxPageIndex)]
        public int PageIndex { get; set; } = pageIndex;

        [Range(1, MaxPageSize)]
        public int PageSize { get; set; } = pageSize;

        public string? Filter { get; set; }

        public string? Sorting { get; set; }

        public PageQueryDto(int pageIndex, int pageSize, string? filter = null, string? sorting = null)
            : this(pageIndex, pageSize)
        {
            Filter = filter;
            Sorting = sorting;
        }
    }
}
