namespace Serveo.Application.Dtos
{
    public class Select2Result
    {
        public IEnumerable<Select2ItemDto> Results { get; set; } = default!;
        public Select2Pagination Pagination { get; set; } = default!;

    }
    public class Select2Pagination
    {
        public bool More { get; set; } = false;
    }
    public class Select2ItemDto(string id, string text)
    {
        public string Id { get; internal set; } = id;
        public string Text { get; internal set; } = text;
    }
}
