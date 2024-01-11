using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tangy_Models;
using Tangy_Server.Service.IService;

namespace Tangy_Server.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> Add(RoleDTO roleDto)
        {
            var identityRole = new IdentityRole
            {
                Name = roleDto.Name,
                NormalizedName = _roleManager.NormalizeKey(roleDto.Name)
            };

            var result = await _roleManager.CreateAsync(identityRole);

            if (!result.Succeeded) return false;

            return true;

        }

        public async Task<bool> Delete(RoleDTO roleDto)
        {
            var identityRole = await Find(roleDto.Id);

            if (identityRole == null) return false;

            //ตรวจสอบมีผู้ใช้บทบาทนี้หรือไม่
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleDto.Name);
            if (usersInRole.Count != 0) return false;

            var result = await _roleManager.DeleteAsync(identityRole);

            if (!result.Succeeded) return false;

            return true;
        }

        public async Task<IdentityRole> Find(string id)
        {
            var identityRole = await _roleManager.FindByIdAsync(id);
            return identityRole;
        }

        public async Task<List<RoleDTO>> GetAll()
        {
            var result = await _roleManager.Roles.ToListAsync();

            var roleDTO = new List<RoleDTO>();

            foreach (var role in result) roleDTO.Add(new RoleDTO { Id = role.Id, Name = role.Name });

            return roleDTO;
        }

        public async Task<bool> Update(RoleDTO roleDto)
        {
            //ตรวจสอบกรณีแก้ซ้ำกับที่มีอยู่แล้ว
            var repeat = await _roleManager.FindByNameAsync(roleDto.Name);
            if (repeat != null) return false;

            var identityRole = await Find(roleDto.Id);

            if (identityRole == null) return false;

            identityRole.Name = roleDto.Name;
            identityRole.NormalizedName = _roleManager.NormalizeKey(roleDto.Name);

            var result = await _roleManager.UpdateAsync(identityRole);

            if (!result.Succeeded) return false;

            return true;
        }

    }

}
