using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serveo.Domain.Abstractions
{
    public class CamelCaseEnumConverter : JsonStringEnumConverter
    {
        public CamelCaseEnumConverter() : base(JsonNamingPolicy.CamelCase) { }
    }
}
