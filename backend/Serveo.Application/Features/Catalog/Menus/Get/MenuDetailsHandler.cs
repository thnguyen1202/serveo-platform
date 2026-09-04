using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Common.Results;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Features.Identity.Auth.Login;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Menus.Get
{
    public sealed class MenuDetailsHandler(
        IUnitOfWork unitOfWork,
        CommandMapper mapper
    ) : ICommandHandler<MenuDetailsCommand, ICommandResult<MenuDto>>
    {
        //public async Task<ICommandResult<MenuDto>> HandleAsync(MenuDetailsCommand request, CancellationToken ct)
        //{
        //    var entity = await unitOfWork.Menus.FindAsync([request.MenuId], ct);
        //    if (entity is null)
        //        return CommandResult<MenuDto>.Failure(
        //            CommandErrors.NotFound(ErrorCodes.Menu.NotFound, "Menu not found."));

        //    var result = mapper.ToDto(entity);
        //    if (request.IsFull)
        //    {
        //        result.Categories = mapper.ToDtoList( await unitOfWork.Categories.GetCategoriesFromMenuAsync(entity.Id, request.IsFull, ct));
        //    }
               
        //    return CommandResult<MenuDto>.Success(result);
        //}

        private async Task<Menu?> GetByIdAsync(Guid menuId, bool isFull = false, CancellationToken ct = default)
        {
            var query = unitOfWork.Menus.Query();

            if (isFull)
            {
                // Load Menu -> MenuCategories -> Category -> Products
                query = query
                    .Include(m => m.MenuCategories)
                        .ThenInclude(mc => mc.Category)
                            .ThenInclude(c => c.Products);
            }

            return await query.FirstOrDefaultAsync(m => m.Id == menuId, ct);
        }

        public async Task<ICommandResult<MenuDto>> HandleAsync(MenuDetailsCommand request, CancellationToken ct)
        {
            // Lấy toàn bộ dữ liệu cần thiết trong 1 Query duy nhất
            var entity = await GetByIdAsync(request.MenuId, request.IsFull, ct);

            if (entity is null)
                return CommandResult<MenuDto>.Failure(
                    CommandErrors.NotFound(ErrorCodes.Menu.NotFound, "Menu not found."));

            // AutoMapper tự động map Categories & Products nếu entity đã có dữ liệu
            var result = mapper.ToDto(entity);

            if (request.IsFull)
            {
                result.Categories = [.. entity.MenuCategories.Select(x => mapper.ToDto(x.Category))];
            }
            

            return CommandResult<MenuDto>.Success(result);
        }
    }
}
