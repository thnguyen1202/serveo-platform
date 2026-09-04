using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application.Features.Catalog.Products.Create
{
    public sealed class CreateProductHandler(
        IUnitOfWork unitOfWork,
        CommandMapper mapper
    ) : ICommandHandler<CreateProductCommand, ICommandResult<CreateProductResult>>
    {
        public async Task<ICommandResult<CreateProductResult>> HandleAsync(CreateProductCommand request, CancellationToken ct)
        {
            var entity = new Product();
            unitOfWork.SetValues(entity, request);

            var result = await unitOfWork.ExecuteAsync(async () =>
            {
                // create Product
                unitOfWork.Add(entity);

                // create MenuProduct
                if (request.MenuId.HasValue)
                {
                    var maxDisplayOrder = await unitOfWork.Set<MenuProduct>()
                        .Where(x => x.MenuId == request.MenuId)
                        .Select(x => (int?)x.DisplayOrder)
                        .MaxAsync() ?? 0;

                    unitOfWork.Add(new MenuProduct
                    {
                        MenuId = request.MenuId.Value,
                        ProductId = entity.Id,
                        PriceOverride = entity.Price,
                        IsVisible = true,
                        DisplayOrder = ++maxDisplayOrder
                    });
                }

                return mapper.ToResult(entity);
            }, ct);

            return CommandResult<CreateProductResult>.Success(result);
        }
    }
}
