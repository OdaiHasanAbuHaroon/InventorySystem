using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Mappers.Mappers.Identity
{
    public class UserProfileMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {

        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;

        public UserProfileModel DataToProfile(User user)
        {
            var userProfile = new UserProfileModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                SmsEnabled = user.SmsEnabled,
                ImageId = user.ImageId,
                ImageLink = user.Image != null ? $"/AttachmentStore?id={user.Image.Id}" : null,
            };

            Dictionary<long, ModuleDataModel> moduleDataList = [];

            // Add FeaturePermissions from UserFeaturePermissions
            foreach (UserFeaturePermission? userFeaturePermission in user.UserFeaturePermissions.Where(x => x.IsDeleted == false).ToList())
            {
                if (userFeaturePermission.FeaturePermission?.Feature?.Module != null)
                {
                    Module? module = userFeaturePermission.FeaturePermission.Feature.Module;
                    Feature? feature = userFeaturePermission.FeaturePermission.Feature;
                    Permission? permission = userFeaturePermission.FeaturePermission.Permission;

                    if (!moduleDataList.TryGetValue(module.Id, out ModuleDataModel? moduleData))
                    {
                        moduleData = new ModuleDataModel
                        {
                            Id = module.Id,
                            Name = module.Name,
                            Features = []
                        };
                        moduleDataList[module.Id] = moduleData;
                    }

                    FeatureDataModel? featureData = moduleData.Features.FirstOrDefault(f => f.Id == feature.Id);
                    if (featureData == null)
                    {
                        featureData = new FeatureDataModel
                        {
                            Id = feature.Id,
                            Name = feature.Name,
                            FeaturePermissions = [],
                            ModuleId = module.Id,
                        };
                        moduleData.Features.Add(featureData);
                    }

                    if (permission != null)
                    {
                        featureData.FeaturePermissions.Add(
                            new FeaturePermissionDataModel
                            {
                                Id = userFeaturePermission.FeaturePermissionId,
                                FeatureId = feature.Id,
                                PermissionId = permission.Id,
                                Permission = new PermissionDataModel
                                {
                                    Id = permission.Id,
                                    Name = permission.Name,
                                    Description = permission.Description
                                }
                            }
                        );
                    }
                }
            }

            // Add FeaturePermissions from SecurityGroups
            foreach (UserSecurityGroup? userSecurityGroup in user.UserSecurityGroups.Where(x => x.IsDeleted == false).ToList())
            {
                foreach (GroupFeaturePermission? groupFeaturePermission in userSecurityGroup.SecurityGroup?.GroupFeaturePermissions ?? Enumerable.Empty<GroupFeaturePermission>())
                {
                    if (groupFeaturePermission.FeaturePermission?.Feature?.Module != null)
                    {
                        Module? module = groupFeaturePermission.FeaturePermission.Feature.Module;
                        Feature? feature = groupFeaturePermission.FeaturePermission.Feature;
                        Permission? permission = groupFeaturePermission.FeaturePermission.Permission;

                        if (!moduleDataList.TryGetValue(module.Id, out ModuleDataModel? moduleData))
                        {
                            moduleData = new ModuleDataModel
                            {
                                Id = module.Id,
                                Name = module.Name,
                                Features = []
                            };

                            moduleDataList[module.Id] = moduleData;
                        }

                        FeatureDataModel? featureData = moduleData.Features.FirstOrDefault(feature => feature.Id == feature.Id);
                        if (featureData == null)
                        {
                            featureData = new FeatureDataModel
                            {
                                Id = feature.Id,
                                Name = feature.Name,
                                FeaturePermissions = new List<FeaturePermissionDataModel>(),
                                ModuleId = module.Id,
                            };
                            moduleData.Features.Add(featureData);
                        }

                        if (permission != null && !featureData.FeaturePermissions.Any(featurePermission => featurePermission.Id == groupFeaturePermission.FeaturePermissionId))
                        {
                            featureData.FeaturePermissions.Add(
                                new FeaturePermissionDataModel
                                {
                                    Id = groupFeaturePermission.FeaturePermissionId,
                                    FeatureId = feature.Id,
                                    PermissionId = permission.Id,
                                    Permission = new PermissionDataModel
                                    {
                                        Id = permission.Id,
                                        Name = permission.Name,
                                        Description = permission.Description
                                    }
                                }
                            );
                        }
                    }
                }
            }

            userProfile.Modules = moduleDataList.Values.ToList();
            userProfile.Roles.AddRange(user.UserRoles.Select(r => r.Role!.RoleName));

            return userProfile;
        }
    }
}
