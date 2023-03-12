#region References
using AutoMapper;
using BookingManagementCommon.DTO;
using BookingManagementContracts.Mapper;
using BookingManagementDomain.EntityModels;
#endregion

#region Namespace
namespace BookingManagementApplication.Mapper
{
    public class EntityMapper : IEntityMapper
    {
        /// <summary>
        /// The mapper configuration
        /// </summary>
        private MapperConfiguration _mapperConfiguration;
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMapper"/> class.
        /// </summary>
        public EntityMapper()
        {
            _configureMapper();
            _createMapper();
        }

        /// <summary>
        /// Configures the mapper.
        /// </summary>
        private void _configureMapper()
        {
            _mapperConfiguration = new MapperConfiguration(mapConfig =>
            {
                #region Booking mapping.
                mapConfig.CreateMap<Booking, BookingDto>().ReverseMap();
                #endregion

                #region User mapping.
                mapConfig.CreateMap<User, UserDto>()
               .ForMember(m => m.Id, t => t.MapFrom(d => d.Id))
               .ForMember(m => m.UniqueId, t => t.MapFrom(d => d.UniqueId))
               .ReverseMap();
                #endregion

                #region Role mapping.
                mapConfig.CreateMap<Role, RoleDto>()
               .ForMember(m => m.Id, t => t.MapFrom(d => d.Id))
               .ForMember(m => m.UniqueId, t => t.MapFrom(d => d.UniqueId))
               .ForMember(m => m.RoleName, t => t.MapFrom(d => d.RoleName))
               .ForMember(m => m.RoleDescription, t => t.MapFrom(d => d.RoleDescription))
               .ForMember(m => m.IsActive, t => t.MapFrom(d => d.IsActive))
               .ForMember(m => m.CreatedBy, t => t.MapFrom(d => d.CreatedBy))
               .ForMember(m => m.CreatedDateTime, t => t.MapFrom(d => d.CreatedDateTime))
               .ForMember(m => m.UpdatedBy, t => t.MapFrom(d => d.UpdatedBy))
               .ForMember(m => m.UpdatedDateTime, t => t.MapFrom(d => d.UpdatedDateTime))
               .ForMember(m => m.Permissions, t => t.MapFrom(d => d.Permissions))
               .ReverseMap();
                #endregion

                #region Permission mapping.
                mapConfig.CreateMap<Permission, PermissionDto>()
               .ForMember(m => m.Id, t => t.MapFrom(d => d.Id))
               .ForMember(m => m.UniqueId, t => t.MapFrom(d => d.UniqueId))
               .ForMember(m => m.RoleId, t => t.MapFrom(d => d.RoleId))
               .ForMember(m => m.PermissionName, t => t.MapFrom(d => d.PermissionName))
               .ForMember(m => m.PermissionDescription, t => t.MapFrom(d => d.PermissionDescription))
               .ForMember(m => m.IsActive, t => t.MapFrom(d => d.IsActive))
               .ForMember(m => m.CreatedBy, t => t.MapFrom(d => d.CreatedBy))
               .ForMember(m => m.CreatedDateTime, t => t.MapFrom(d => d.CreatedDateTime))
               .ForMember(m => m.UpdatedBy, t => t.MapFrom(d => d.UpdatedBy))
               .ForMember(m => m.UpdatedDateTime, t => t.MapFrom(d => d.UpdatedDateTime))
               .ForMember(m => m.Role, t => t.MapFrom(d => d.Role))
               .ReverseMap();
                #endregion
            });
        }

        /// <summary>
        /// Creates the mapper.
        /// </summary>
        private void _createMapper()
        {
            _mapper = _mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
#endregion