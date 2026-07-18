using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serveo.Application.Dtos;
using Serveo.Application.Services.Administration.Roles.Dto;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Interfaces;

namespace Serveo.Application.Services.Administration.Roles
{
    using CrudEntity = Role;
    using EntityDto = RoleDto;

    public class RoleService : IRoleService, IService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private const string _entityName = nameof(RefreshToken);
        private readonly DbSet<CrudEntity> _entities;

        public RoleService(ILogger<RoleService> logger, IMapper mapper, IUnitOfWork unitOfWork)
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
        //    if (await IsDuplicateNameAsync(input.Name, _unitOfWork.Session.TenantId))
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

        //    if (await IsDuplicateNameAsync(input.Name, _unitOfWork.Session.TenantId, input.Id))
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

        public async Task<bool> IsDuplicateNameAsync(string name, Guid? tenantId = null, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            name = name.Trim();

            return await _entities.AnyAsync(x =>
                x.Name == name &&
                (!tenantId.HasValue || x.TenantId == tenantId.Value) &&
                (!id.HasValue || x.Id != id.Value)
            );
        }

        public async Task<List<string?>> GetPermissionsAsync(Guid roleId)
        {
            return await _unitOfWork.Set<RoleClaim>()
                .Where(x => x.RoleId == roleId && x.ClaimType == "Permission")
                .Select(s => s.ClaimValue)
                .ToListAsync();
        }

        //public async Task<CommandResult> UpdateClaimsAsync(UpdateClaimsDto input, CancellationToken ct)
        //{
        //    if (input.Id == null)
        //        throw new ArgumentException("RoleId is required");

        //    var claims = await _unitOfWork.Set<RoleClaim>()
        //        .Where(x => x.RoleId == input.Id && x.ClaimType == "Permission")
        //        .ToListAsync(ct);

        //    var current = claims.Select(x => x.ClaimValue).ToHashSet();
        //    var inputSet = input.Claims.ToHashSet();

        //    var insert = input.Claims.Except(current);
        //    var delete = claims.Where(x => x.ClaimValue != null && !inputSet.Contains(x.ClaimValue));

        //    await _unitOfWork.ExecuteAsync(async () =>
        //    {
        //        foreach (var permission in insert)
        //        {
        //            _unitOfWork.Add(new RoleClaim
        //            {
        //                RoleId = input.Id ?? Guid.Empty,
        //                ClaimType = "Permission",
        //                ClaimValue = permission,
        //                IsGranted = true
        //            });
        //        }

        //        foreach (var roleClaim in delete)
        //        {
        //            _unitOfWork.Remove(roleClaim);
        //        }

        //        await Task.CompletedTask;
        //    }, ct);

        //    return CommandResult.Success;
        //}
    }
}