﻿using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class FeaturePermissionRepository : GenericRepository<FeaturePermission>, IFeaturePermissionRepository
    {
        public FeaturePermissionRepository(DatabaseContext context) : base(context) { }
    }
}