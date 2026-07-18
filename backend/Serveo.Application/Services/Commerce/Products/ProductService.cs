using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serveo.Application.Dtos;
using Serveo.Application.Services.Commerce.Products.Dto;
using Serveo.Domain.Entities.Catalog;
using Serveo.Domain.Interfaces;

namespace Serveo.Application.Services.Commerce.Products
{
    using CrudEntity = Product;
    using EntityDto = ProductDto;

    public class ProductService : IProductService, IService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<CrudEntity> _entities;

        private const string _entityName = nameof(Product);

        public ProductService(ILogger<ProductService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _entities = _unitOfWork.Set<CrudEntity>();
        }

        public async Task<PagedResult<EntityDto>> PageAsync(PageQueryDto input)
        {
            var query = _entities.AsQueryable();

            #region Sorting
            query = input.Sorting switch
            {
                _ => query.OrderByDescending(o => o.Id),
            };
            #endregion

            #region Filter
            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(input.Filter));
            }
            #endregion

            #region Paged
            var itemCount = await query.CountAsync();
            var items = query
                .Skip((input.PageIndex - 1) * input.PageSize)
                .Take(input.PageSize)
                .AsEnumerable();
            #endregion

            return new PagedResult<EntityDto>(_mapper.Map<IReadOnlyList<EntityDto>>(items), itemCount);
        }

        //public async Task<CommandResult> CreateAsync(CreateDto input, CancellationToken ct = default)
        //{
        //    if (await IsDuplicateNameAsync(input.Name, input.CategoryId))
        //        return CommandResult.Failed(new CommandError { Description = $"The '{input.Name}' already exists." });

        //    var entityDto = await _unitOfWork.ExecuteAsync(async () =>
        //    {
        //        var entity = new CrudEntity { };
        //        _unitOfWork.SetValues(entity, input);
        //        _unitOfWork.Add(entity);
        //        return _mapper.Map<EntityDto>(entity);
        //    }, ct);

        //    var result = CommandResult.Success;
        //    result.Data = entityDto;
        //    return result;
        //}

        //public async Task<CommandResult> UpdateAsync(UpdateDto input, CancellationToken ct = default)
        //{
        //    var entity = await _entities.FirstOrDefaultAsync(x => x.Id == input.Id, ct);
        //    if (entity == null)
        //        return CommandResult.Failed(new CommandError { Description = $"The {_entityName} not found." });

        //    if (await IsDuplicateNameAsync(input.Name, input.CategoryId, input.Id))
        //        return CommandResult.Failed(new CommandError { Description = $"The '{input.Name}' already exists." });

        //    _unitOfWork.SetValues(entity, input);
        //    _unitOfWork.Update(entity);
        //    await _unitOfWork.SaveChangesAsync(ct);

        //    var result = CommandResult.Success;
        //    result.Data = _mapper.Map<EntityDto>(entity);
        //    return result;
        //}

        //public async Task<CommandResult> DeleteAsync(object? id, CancellationToken ct = default)
        //{
        //    var entity = await _entities.FindAsync([id], ct);
        //    if (entity == null) return CommandResult.Failed(new CommandError { Description = $"The {_entityName} not found." });

        //    await _unitOfWork.ExecuteAsync(async () =>
        //    {
        //        _unitOfWork.Remove(entity);
        //    }, ct);

        //    return CommandResult.Success;
        //}

        //public async Task<CommandResult> UpdateStatusAsync(UpdateStatusDto input, CancellationToken ct = default)
        //{
        //    var entity = await _entities.FindAsync([input.Id], ct);
        //    if (entity == null)
        //        return CommandResult.Failed(new CommandError { Description = $"The {_entityName} not found." });

        //    _unitOfWork.SetValues(entity, input);
        //    _unitOfWork.Update(entity);
        //    await _unitOfWork.SaveChangesAsync(ct);

        //    var result = CommandResult.Success;
        //    result.Data = _mapper.Map<EntityDto>(entity);
        //    return result;
        //}

        public async Task<EntityDto?> FindAsync(object? id, CancellationToken ct = default)
        {
            var entity = await _entities.FindAsync([id], ct);
            return _mapper.Map<EntityDto>(entity);
        }

        public async Task<bool> IsDuplicateNameAsync(string name, Guid? categoryId = null, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            name = name.Trim();

            return await _entities.AnyAsync(x =>
                x.Name == name &&
                (!categoryId.HasValue || x.CategoryId == categoryId.Value) &&
                (!id.HasValue || x.Id != id.Value)
            );
        }
    }
}