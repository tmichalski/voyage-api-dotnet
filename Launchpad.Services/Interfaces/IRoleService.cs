﻿using Launchpad.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Launchpad.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRoleAsync(RoleModel model);

        IEnumerable<RoleModel> GetRoles();
    
        Task AddClaimAsync(RoleModel role, ClaimModel claim); 
    }
}
