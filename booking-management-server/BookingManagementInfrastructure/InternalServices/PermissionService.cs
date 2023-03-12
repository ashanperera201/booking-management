#region References
using BookingManagementCommon.DTO;
using BookingManagementContracts.Mapper;
using BookingManagementContracts.Repositories;
using BookingManagementContracts.Services;
using BookingManagementDomain.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
#endregion

#region Namespace
namespace BookingManagementInfrastructure.InternalServices
{
    public class PermissionService : IPermissionService
    {

        /// <summary>
        /// The permission repository
        /// </summary>
        private readonly IPermissionRepository _permissionRepository;
        /// <summary>
        /// The entity mapper
        /// </summary>
        private readonly IEntityMapper _entityMapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="permissionRepository">The permission repository.</param>
        /// <param name="entityMapper">The entity mapper.</param>
        public PermissionService(IPermissionRepository permissionRepository, IEntityMapper entityMapper)
        {
            _permissionRepository = permissionRepository;
            _entityMapper = entityMapper;
        }

        /// <summary>
        /// Deletes the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeletePermissionAsync(string permissionId, CancellationToken cancellationToken = default)
        {
            var result = await _permissionRepository.DeletePermissionAsync(permissionId, cancellationToken);
            return result;
        }

        /// <summary>
        /// Gets all permissions asynchronous.
        /// </summary>
        /// <param name="pagination">The pagination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<PaginationResponseDto<PermissionDto>> GetAllPermissionsAsync(PaginationDto pagination, CancellationToken cancellationToken = default)
        {
            var permissions = _permissionRepository.GetAllPermissionsAsync(cancellationToken);
            var totalCount = await permissions.CountAsync();
            var permissionList = await (from permission in permissions select permission)
                               .OrderByDescending(x => x.CreatedDateTime)
                               .Skip((pagination.Page - 1) * pagination.PageSize)
                               .Take(pagination.PageSize)
                               .ToListAsync();


            return new PaginationResponseDto<PermissionDto>
            {
                Page = pagination.Page,
                total = totalCount,
                Records = _entityMapper.Map<List<Permission>, List<PermissionDto>>(permissionList)
            };
        }

        /// <summary>
        /// Gets the permission asynchronous.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<PermissionDto?> GetPermissionAsync(string permissionId, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionRepository.GetPermissionAsync(permissionId, cancellationToken);
            if (permission != null)
            {
                return _entityMapper.Map<Permission, PermissionDto>(permission);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Saves the permission asynchronous.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<PermissionDto> SavePermissionAsync(PermissionDto permission, CancellationToken cancellationToken = default)
        {
            permission.UniqueId = Guid.NewGuid();
            permission.CreatedDateTime = DateTime.UtcNow;
            var mappedEntitty = _entityMapper.Map<PermissionDto, Permission>(permission);
            var savedResult = await _permissionRepository.SavePermissionAsync(mappedEntitty, cancellationToken);
            return _entityMapper.Map<Permission, PermissionDto>(savedResult);
        }

        /// <summary>
        /// Updates the permission asynchronous.
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<PermissionDto> UpdatePermissionAsync(PermissionDto permission, CancellationToken cancellationToken = default)
        {
            var mappedEntitty = _entityMapper.Map<PermissionDto, Permission>(permission);
            permission.UpdatedDateTime = DateTime.UtcNow;
            var updatedResult = await _permissionRepository.UpdatePermissinAsync(mappedEntitty, cancellationToken);
            return _entityMapper.Map<Permission, PermissionDto>(updatedResult);
        }
    }
}
#endregion