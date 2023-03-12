#region References
using Microsoft.EntityFrameworkCore;
using BookingManagementCommon.DTO;
using BookingManagementContracts.Mapper;
using BookingManagementContracts.Repositories;
using BookingManagementContracts.Services;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementInfrastructure.InternalServices
{
    public class RoleService : IRoleService
    {
        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository _roleRepository;
        /// <summary>
        /// The entity mapper
        /// </summary>
        private readonly IEntityMapper _entityMapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="entityMapper">The entity mapper.</param>
        public RoleService(IRoleRepository roleRepository, IEntityMapper entityMapper)
        {
            _roleRepository = roleRepository;
            _entityMapper = entityMapper;
        }

        public async Task<bool> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var result = await _roleRepository.DeleteRoleAsync(roleId, cancellationToken);
            return result;
        }

        /// <summary>
        /// Gets all Roles asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<PaginationResponseDto<RoleDto>> GetAllRolesAsync(PaginationDto pagination, CancellationToken cancellationToken = default)
        {
            var roles = _roleRepository.GetRoleDetailsAsync(cancellationToken);
            var totalCount = await roles.CountAsync();
            var roleList = await (from role in roles select role)
                               .OrderByDescending(x => x.CreatedDateTime)
                               .Skip((pagination.Page - 1) * pagination.PageSize)
                               .Take(pagination.PageSize)
                               .ToListAsync();


            return new PaginationResponseDto<RoleDto>
            {
                Page = pagination.Page,
                total = totalCount,
                Records = _entityMapper.Map<List<Role>, List<RoleDto>>(roleList)
            };
        }

        /// <summary>
        /// Gets the Role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var result = await _roleRepository.GetRoleAsync(roleId, cancellationToken);
            return _entityMapper.Map<Role, RoleDto>(result);
        }

        /// <summary>
        /// Saves the Role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto> SaveRoleAsync(RoleDto role, CancellationToken cancellationToken = default)
        {
            role.UniqueId = Guid.NewGuid();
            role.CreatedDateTime = DateTime.UtcNow;
            var mappedEntitty = _entityMapper.Map<RoleDto, Role>(role);
            var savedResult = await _roleRepository.SaveRoleAsync(mappedEntitty, cancellationToken);
            return _entityMapper.Map<Role, RoleDto>(savedResult);
        }

        /// <summary>
        /// Updates the Role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto> UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken = default)
        {
            var mappedEntitty = _entityMapper.Map<RoleDto, Role>(role);
            role.UpdatedDateTime = DateTime.UtcNow;
            var savedResult = await _roleRepository.UpdateRoleAsync(mappedEntitty, cancellationToken);
            return _entityMapper.Map<Role, RoleDto>(savedResult);
        }
    }
}
#endregion