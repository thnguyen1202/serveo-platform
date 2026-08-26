using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed class MyMenuHandler(
        IUnitOfWork unitOfWork
    ) : ICommandHandler<MyMenuCommand, IEnumerable<MenuOption>>
    {
        public async Task<IEnumerable<MenuOption>> HandleAsync(MyMenuCommand request, CancellationToken ct)
        {
            var entities = await unitOfWork.Menus.Query()
                //.Include(x => x.MenuProducts)
                .Where(x => x.BusinessId == unitOfWork.Session.BusinessId && x.IsActive == true)
                .Select(s => new MenuOption { Id = s.Id, Name = s.Name, ItemCount = s.MenuProducts.Count })
                .ToListAsync(ct);

            return entities;
        }
    }
}
