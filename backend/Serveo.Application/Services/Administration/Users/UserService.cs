using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serveo.Application.Dtos;
using Serveo.Application.Services.Administration.Users.Dto;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Interfaces;

namespace Serveo.Application.Services.Administration.Users
{
    using CrudEntity = User;
    using EntityDto = UserDto;

    public class UserService : IUserService, IService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<RefreshToken> _roleManager;
        private readonly IdentityOptions _identityOptions;

        private const string _entityName = nameof(User);
        private readonly DbSet<CrudEntity> _entities;

        public UserService(ILogger<UserService> logger, IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<RefreshToken> roleManager, IOptions<IdentityOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _entities = _unitOfWork.Set<CrudEntity>();
            _userManager = userManager;
            _roleManager = roleManager;
            _identityOptions = options.Value;
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
                //query = query.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(input.Filter));
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

        //public async Task<ICommandResult> CreateAsync(CreateDto input, CancellationToken ct = default)
        //{
        //    if (await IsDuplicateEmailAsync(input.Email))
        //        return CommandResult.Failed(new CommandError { Description = $"The '{input.Email}' already exists." });

        //    var user = new User();
        //    _unitOfWork.SetValues(user, input);
        //    user.EmailConfirmed = !_identityOptions.SignIn.RequireConfirmedEmail;
        //    user.TimeZoneId ??= TimeZoneInfo.Local.Id;
        //    //user.TenantId = diner?.TenantId;
        //    user.SetNormalizedNames();

        //    var identityResult = await _userManager.CreateAsync(user, input.Password ?? "Qwe@123");
        //    if (!identityResult.Succeeded)
        //        return CommandResult.Failed(identityResult.Errors);

        //    var result = CommandResult.Success;
        //    result.Data = _mapper.Map<EntityDto>(user); ;
        //    return result;
        //}

        //public async Task<CommandResult> UpdateAsync(UpdateDto input, CancellationToken ct = default)
        //{
        //    var entity = await _entities.FindAsync([input.Id], ct);
        //    if (entity == null)
        //        return CommandResult.Failed(new CommandError { Description = $"The {_entityName} not found." });

        //    if (await IsDuplicateEmailAsync(input.Email, input.Id))
        //        return CommandResult.Failed(new CommandError { Description = $"The '{input.Email}' already exists." });

        //    _unitOfWork.SetValues(entity, input);
        //    if (!string.IsNullOrWhiteSpace(input.Password))
        //        entity.PasswordHash = _userManager.PasswordHasher.HashPassword(entity, input.Password);

        //    var identityResult = await _userManager.UpdateAsync(entity);
        //    if (!identityResult.Succeeded)
        //        return CommandResult.Failed(identityResult.Errors);

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

        public async Task<bool> IsDuplicateUserNameAsync(string name, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            name = name.Trim().ToUpperInvariant();

            return await _userManager.Users.AnyAsync(x =>
                x.NormalizedUserName == name &&
                (!id.HasValue || x.Id != id.Value)
            );
        }

        public async Task<bool> IsDuplicateEmailAsync(string email, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            email = email.Trim().ToUpperInvariant();

            return await _userManager.Users.AnyAsync(x =>
                x.NormalizedEmail == email &&
                (!id.HasValue || x.Id != id.Value)
            );
        }
    }
}
