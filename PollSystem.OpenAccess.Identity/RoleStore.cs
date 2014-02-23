using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace PollSystem.OpenAccess.Identity
{
    public class RoleStore: IRoleStore<IdentityRole>
    {
        private PollSystemDbCotnext dbCotnext;

        public RoleStore()
        {
            this.dbCotnext = new PollSystemDbCotnext();
        }

        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("Craeting a null role is not allowed.", "role");
            }

            this.dbCotnext.Add(role);
            this.dbCotnext.SaveChanges();

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("Role must not be null.", "role");
            }

            IdentityRole attachedRole = this.dbCotnext.AttachCopy(role);
            this.dbCotnext.Delete(attachedRole);
            this.dbCotnext.SaveChanges();

            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                throw new ArgumentNullException("Cannot match by null or empty roleId.", "roleId");
            }
            IdentityRole matchedIdentity = this.dbCotnext.IdentityRoles.FirstOrDefault(x=>x.Id == roleId);

            return Task.FromResult<IdentityRole>(matchedIdentity);
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("Cannot match by null or empty roleName.", "roleName");
            }
            IdentityRole matchedIdentity = this.dbCotnext.IdentityRoles.FirstOrDefault(x => x.Name == roleName);

            return Task.FromResult<IdentityRole>(matchedIdentity);
        }

        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("Cannot update a null role.", "null");
            }

            this.dbCotnext.AttachCopy(role);
            this.dbCotnext.SaveChanges();

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (this.dbCotnext != null)
            {
                this.dbCotnext.Dispose();
            }
        }
    }
}
