using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;
using InventorySystem.Data.Repositories.IRepositories.Core;
using InventorySystem.Data.Repositories.IRepositories.Identity;
using InventorySystem.Mappers;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventorySystem.ServiceImplementation.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IHostEnvironment HostEnvironment { get; }

        ILoggerFactory LoggerFactory { get; }

        IConfiguration Configuration { get; }

        DatabaseContext Context { get; }

        MapperService MapperService { get; }

        Task<int> CompleteAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        #region IRepositories

        #region Core

        IPersonRepository PersonRepository { get; }

        IAttachmentRepository AttachmentRepository { get; }

        ILanguageRepository LanguageRepository { get; }

        ITimeZoneInformationRepository TimeZoneInformationRepository { get; }

        #endregion

        #region Identity

        IUserRepository UserRepository { get; }

        ITwofactorRepository TwofactorRepository { get; }

        IRoleRepository RoleRepository { get; }

        IUserRoleRepository UserRoleRepository { get; }

        IFeaturePermissionRepository FeaturePermissionRepository { get; }

        IUserFeaturePermissionRepository UserFeaturePermissionRepository { get; }

        IFeatureRepository FeatureRepository { get; }

        IPermissionRepository PermissionRepository { get; }

        IModuleRepository ModuleRepository { get; }

        IUserModuleRepository UserModuleRepository { get; }

        ISecurityGroupRepository SecurityGroupRepository { get; }

        IUserSecurityGroupRepository UserSecurityGroupRepository { get; }

        IGroupFeaturePermissionRepository GroupFeaturePermissionRepository { get; }

        #endregion

        #region Business

        IBrandRepository BrandRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IItemRepository ItemRepository { get; }

        IItemStatusRepository ItemStatusRepository { get; }

        ILocationRepository LocationRepository { get; }

        IManufacturerRepository ManufacturerRepository { get; }

        ISerialNumberRepository SerialNumberRepository { get; }

        ISupplierRepository SupplierRepository { get; }

        #endregion

        #endregion
    }
}
