using System;
using System.Collections.Generic;
using System.Text;

namespace Serveo.Application.Features.Catalog.Menus
{
    public class MenuOption
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public int ItemCount { get; init; }
    }
}
