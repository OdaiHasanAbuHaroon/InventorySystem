using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Mappers.Mappers;
using InventorySystem.Mappers.Mappers.Identity;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Mappers
{
    public class MapperService(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;

        #region Mappers

        #region Core

        private PersonMapper? personMapper;
        public PersonMapper PersonMapper => personMapper ??= new PersonMapper(_httpContextDataProvider, _loggerFactory);

        private AttachmentBackupMapper? attachmentBackupMapper;
        public AttachmentBackupMapper AttachmentBackupMapper => attachmentBackupMapper ??= new AttachmentBackupMapper(_httpContextDataProvider, _loggerFactory);

        private AttachmentMapper? attachmentMapper;
        public AttachmentMapper AttachmentMapper => attachmentMapper ??= new AttachmentMapper(_httpContextDataProvider, _loggerFactory);

        private TimeZoneInformationMapper? timeZoneInformationMapper;
        public TimeZoneInformationMapper TimeZoneInformationMapper => timeZoneInformationMapper ??= new TimeZoneInformationMapper(_httpContextDataProvider, _loggerFactory);

        private LanguageMapper? languageMapper;
        public LanguageMapper LanguageMapper => languageMapper ??= new LanguageMapper(_httpContextDataProvider, _loggerFactory);

        private CurrencyMapper? currencyMapper;
        public CurrencyMapper CurrencyMapper => currencyMapper ??= new CurrencyMapper(_httpContextDataProvider, _loggerFactory);

        private CountryMapper? countryMapper;
        public CountryMapper CountryMapper => countryMapper ??= new CountryMapper(_httpContextDataProvider, _loggerFactory);

        #endregion

        #region Identity

        private FeatureMapper? featureMapper;
        public FeatureMapper FeatureMapper => featureMapper ??= new FeatureMapper(_httpContextDataProvider, _loggerFactory);

        private FeaturePermissionMapper? featurePermissionMapper;
        public FeaturePermissionMapper FeaturePermissionMapper => featurePermissionMapper ??= new FeaturePermissionMapper(_httpContextDataProvider, _loggerFactory);

        private GroupFeaturePermissionMapper? groupFeaturePermissionMapper;
        public GroupFeaturePermissionMapper GroupFeaturePermissionMapper => groupFeaturePermissionMapper ??= new GroupFeaturePermissionMapper(_httpContextDataProvider, _loggerFactory);

        private ModuleMapper? moduleMapper;
        public ModuleMapper ModuleMapper => moduleMapper ??= new ModuleMapper(_httpContextDataProvider, _loggerFactory);

        private PermissionMapper? permissionMapper;
        public PermissionMapper PermissionMapper => permissionMapper ??= new PermissionMapper(_httpContextDataProvider, _loggerFactory);

        private RoleMapper? roleMapper;
        public RoleMapper RoleMapper => roleMapper ??= new RoleMapper(_httpContextDataProvider, _loggerFactory);

        private SecurityGroupMapper? securityGroupMapper;
        public SecurityGroupMapper SecurityGroupMapper => securityGroupMapper ??= new SecurityGroupMapper(_httpContextDataProvider, _loggerFactory);

        private ThemeMapper? themeMapper;
        public ThemeMapper ThemeMapper => themeMapper ??= new ThemeMapper(_httpContextDataProvider, _loggerFactory);

        private TwofactorMapper? twofactorMapper;
        public TwofactorMapper TwofactorMapper => twofactorMapper ??= new TwofactorMapper(_httpContextDataProvider, _loggerFactory);

        private UserFeaturePermissionMapper? userFeaturePermissionMapper;
        public UserFeaturePermissionMapper UserFeaturePermissionMapper => userFeaturePermissionMapper ??= new UserFeaturePermissionMapper(_httpContextDataProvider, _loggerFactory);

        private UserMapper? userMapper;
        public UserMapper UserMapper => userMapper ??= new UserMapper(_httpContextDataProvider, _loggerFactory);

        private UserModuleMapper? userModuleMapper;
        public UserModuleMapper UserModuleMapper => userModuleMapper ??= new UserModuleMapper(_httpContextDataProvider, _loggerFactory);

        private UserRoleMapper? userRoleMapper;
        public UserRoleMapper UserRoleMapper => userRoleMapper ??= new UserRoleMapper(_httpContextDataProvider, _loggerFactory);

        private UserSecurityGroupMapper? userSecurityGroupMapper;
        public UserSecurityGroupMapper UserSecurityGroupMapper => userSecurityGroupMapper ??= new UserSecurityGroupMapper(_httpContextDataProvider, _loggerFactory);

        private UserProfileMapper? userProfileMapper;
        public UserProfileMapper UserProfileMapper => userProfileMapper ??= new UserProfileMapper(_httpContextDataProvider, _loggerFactory);

        #endregion

        #region Business

        private BrandMapper? brandMapper;
        public BrandMapper BrandMapper => brandMapper ??= new BrandMapper(_httpContextDataProvider, _loggerFactory);

        private CategoryMapper? categoryMapper;
        public CategoryMapper CategoryMapper => categoryMapper ??= new CategoryMapper(_httpContextDataProvider, _loggerFactory);

        private ItemMapper? itemMapper;
        public ItemMapper ItemMapper => itemMapper ??= new ItemMapper(_httpContextDataProvider, _loggerFactory);

        private ItemStatusMapper? itemStatusMapper;
        public ItemStatusMapper ItemStatusMapper => itemStatusMapper ??= new ItemStatusMapper(_httpContextDataProvider, _loggerFactory);

        private LocationMapper? locationMapper;
        public LocationMapper LocationMapper => locationMapper ??= new LocationMapper(_httpContextDataProvider, _loggerFactory);

        private ManufacturerMapper? manufacturerMapper;
        public ManufacturerMapper ManufacturerMapper => manufacturerMapper ??= new ManufacturerMapper(_httpContextDataProvider, _loggerFactory);

        private SerialNumberMapper? serialNumberMapper;
        public SerialNumberMapper SerialNumberMapper => serialNumberMapper ??= new SerialNumberMapper(_httpContextDataProvider, _loggerFactory);

        private SupplierMapper? supplierMapper;
        public SupplierMapper SupplierMapper => supplierMapper ??= new SupplierMapper(_httpContextDataProvider, _loggerFactory);

        #endregion

        #endregion
    }
}
