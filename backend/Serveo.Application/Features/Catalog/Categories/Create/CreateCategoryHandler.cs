using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Categories.Create
{
    public sealed class CreateCategoryHandler(
        IUnitOfWork unitOfWork,
        CommandMapper mapper
    ) : ICommandHandler<CreateCategoryCommand, ICommandResult<CreateCategoryResult>>
    {
        public async Task<ICommandResult<CreateCategoryResult>> HandleAsync(CreateCategoryCommand request, CancellationToken ct)
        {
            var entity = new Category();
            unitOfWork.SetValues(entity, request);

            var result = await unitOfWork.ExecuteAsync(async () =>
            {
                // create Category
                unitOfWork.Categories.Add(entity);

                // create MenuCategory
                if (request.MenuId.HasValue)
                {
                    var maxDisplayOrder = await unitOfWork.Set<MenuCategory>()
                        .Where(x => x.MenuId == request.MenuId)
                        .Select(x => (int?)x.DisplayOrder) // Ép kiểu về int? để tránh crash khi tập hợp rỗng
                        .MaxAsync() ?? 0;

                    unitOfWork.Add(new MenuCategory
                    {
                        MenuId = request.MenuId.Value,
                        CategoryId = entity.Id,
                        IsVisible = true,
                        DisplayOrder = ++maxDisplayOrder
                    });
                }

                return mapper.ToResult(entity);
            }, ct);

            return CommandResult<CreateCategoryResult>.Success(result);
        }
    }
}
