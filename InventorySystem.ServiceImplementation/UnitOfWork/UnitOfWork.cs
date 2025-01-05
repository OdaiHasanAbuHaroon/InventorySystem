using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;
using InventorySystem.Data.Repositories.IRepositories.Core;
using InventorySystem.Data.Repositories.IRepositories.Identity;
using InventorySystem.Data.Repositories.Repositories.Business;
using InventorySystem.Data.Repositories.Repositories.Core;
using InventorySystem.Data.Repositories.Repositories.Identity;
using InventorySystem.Mappers;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventorySystem.ServiceImplementation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> LogService;

        public IHostEnvironment HostEnvironment { get; private set; }

        public ILoggerFactory LoggerFactory { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public DatabaseContext Context { get; private set; }

        public MapperService MapperService { get; private set; }

        public UnitOfWork(DatabaseContext context, IConfiguration configuration, ILogger<UnitOfWork> logService, MapperService mapperService, IHostEnvironment hostEnvironment, ILoggerFactory loggerFactory)
        {
            Context = context;
            Configuration = configuration;
            LogService = logService;
            MapperService = mapperService;
            HostEnvironment = hostEnvironment;
            LoggerFactory = loggerFactory;
        }

        #region IRepositories

        #region Business

        #endregion

        #region Core

        private IPersonRepository? personRepository;
        public IPersonRepository PersonRepository => personRepository ??= new PersonRepository(Context);

        private IAttachmentRepository? attachmentRepository;
        public IAttachmentRepository AttachmentRepository => attachmentRepository ??= new AttachmentRepository(Context);

        private ITimeZoneInformationRepository? timeZoneInformationRepository;
        public ITimeZoneInformationRepository TimeZoneInformationRepository => timeZoneInformationRepository ??= new TimeZoneInformationRepository(Context);

        private ILanguageRepository? languageRepository;
        public ILanguageRepository LanguageRepository => languageRepository ??= new LanguageRepository(Context);

        #endregion

        #region Identity

        private IUserRepository? userRepository;
        public IUserRepository UserRepository => userRepository ??= new UserRepository(Context);

        private ITwofactorRepository? twofactorRepository;
        public ITwofactorRepository TwofactorRepository => twofactorRepository ??= new TwofactorRepository(Context);

        private IUserFeaturePermissionRepository? userFeaturePermissionRepository;
        public IUserFeaturePermissionRepository UserFeaturePermissionRepository => userFeaturePermissionRepository ??= new UserFeaturePermissionRepository(Context);

        private IFeatureRepository? featureRepository;
        public IFeatureRepository FeatureRepository => featureRepository ??= new FeatureRepository(Context);

        private IPermissionRepository? permissionRepository;
        public IPermissionRepository PermissionRepository => permissionRepository ??= new PermissionRepository(Context);

        private IModuleRepository? moduleRepository;
        public IModuleRepository ModuleRepository => moduleRepository ??= new ModuleRepository(Context);

        private IUserModuleRepository? userModuleRepository;
        public IUserModuleRepository UserModuleRepository => userModuleRepository ??= new UserModuleRepository(Context);

        private IUserSecurityGroupRepository? userSecurityGroupRepository;
        public IUserSecurityGroupRepository UserSecurityGroupRepository => userSecurityGroupRepository ??= new UserSecurityGroupRepository(Context);

        private ISecurityGroupRepository? securityGroupRepository;
        public ISecurityGroupRepository SecurityGroupRepository => securityGroupRepository ??= new SecurityGroupRepository(Context);

        private IGroupFeaturePermissionRepository? groupFeaturePermissionRepository;
        public IGroupFeaturePermissionRepository GroupFeaturePermissionRepository => groupFeaturePermissionRepository ??= new GroupFeaturePermissionRepository(Context);

        private IRoleRepository? roleRepository;
        public IRoleRepository RoleRepository => roleRepository ??= new RoleRepository(Context);

        private IUserRoleRepository? userRoleRepository;
        public IUserRoleRepository UserRoleRepository => userRoleRepository ??= new UserRoleRepository(Context);

        private IFeaturePermissionRepository? featurePermissionRepository;
        public IFeaturePermissionRepository FeaturePermissionRepository => featurePermissionRepository ??= new FeaturePermissionRepository(Context);

        #endregion

        #region Business

        private IBrandRepository? brandRepository;
        public IBrandRepository BrandRepository => brandRepository ??= new BrandRepository(Context);

        private ICategoryRepository? categoryRepository;
        public ICategoryRepository CategoryRepository => categoryRepository ??= new CategoryRepository(Context);

        private IItemRepository? itemRepository;
        public IItemRepository ItemRepository => itemRepository ??= new ItemRepository(Context);

        private IItemStatusRepository? itemStatusRepository;
        public IItemStatusRepository ItemStatusRepository => itemStatusRepository ??= new ItemStatusRepository(Context);

        private ILocationRepository? locationRepository;
        public ILocationRepository LocationRepository => locationRepository ??= new LocationRepository(Context);

        private IManufacturerRepository? manufacturerRepository;
        public IManufacturerRepository ManufacturerRepository => manufacturerRepository ??= new ManufacturerRepository(Context);

        private ISerialNumberRepository? serialNumberRepository;
        public ISerialNumberRepository SerialNumberRepository => serialNumberRepository ??= new SerialNumberRepository(Context);

        private ISupplierRepository? supplierRepository;
        public ISupplierRepository SupplierRepository => supplierRepository ??= new SupplierRepository(Context);

        #endregion

        #endregion

        public async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Context.Database.BeginTransactionAsync();
        }
    }
}
