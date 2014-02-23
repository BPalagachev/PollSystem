using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using System.Linq;

namespace PollSystem.OpenAccess.Identity
{
    public class UserStore : IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>
    {
        private PollSystemDbCotnext dbContext;

        public UserStore()
        {
            this.dbContext = new PollSystemDbCotnext();
        }

        public Task CreateAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Creating a null user is not allowed.", "user");
            }

            if (this.dbContext.IdentityUsers.FirstOrDefault(x=>x.Id == user.Id) == null)
            {
                this.dbContext.Add(user);
            }
            this.dbContext.SaveChanges();

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            if (user != null)
            {
                IdentityUser attachedUser = this.dbContext.AttachCopy(user);
                this.dbContext.Delete(attachedUser);
                this.dbContext.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Cannot match null or empty userId", "userId");
            }

            IdentityUser matchedUser = this.dbContext.IdentityUsers.FirstOrDefault(x => x.Id == userId);

            if (matchedUser != null)
            {
                IdentityUser detachedUser = dbContext.CreateDetachedCopy(matchedUser);
                return Task.FromResult<IdentityUser>(detachedUser);
            }

            return Task.FromResult<IdentityUser>(null);
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Cannot match null or empty username", "userName");
            }

            IdentityUser matchedUser = this.dbContext.IdentityUsers.FirstOrDefault(x => x.UserName == userName);

            if (matchedUser != null)
            {
                IdentityUser detachedUser = dbContext.CreateDetachedCopy(matchedUser);
                return Task.FromResult<IdentityUser>(detachedUser);
            }

            return Task.FromResult<IdentityUser>(null);
        }

        public Task UpdateAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Cannot update a null user", "user");
            }

            this.dbContext.AttachCopy(user);
            this.dbContext.SaveChanges();
            user = dbContext.CreateDetachedCopy(user);

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user != null)
	        {
                IdentityUser currentUser = this.dbContext.IdentityUsers.FirstOrDefault(x => x.Id == user.Id);
                string passwordHash = currentUser.PasswordHash;
                return Task.FromResult<string>(passwordHash);
	        }
            throw new ArgumentNullException("Cannot get the hashed password of a null user.", "user");
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user != null)
            {
                IdentityUser currentUser = this.dbContext.IdentityUsers.FirstOrDefault(x => x.Id == user.Id);
                string passwordHash = currentUser.PasswordHash;
                var hasPassword = !string.IsNullOrEmpty(passwordHash);

                return Task.FromResult<bool>(hasPassword);
            }
            throw new ArgumentNullException("Cannot check a null user for password..", "user");
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            IdentityUser attachedUser = this.dbContext.AttachCopy(user);
            attachedUser.PasswordHash = passwordHash;
            this.dbContext.SaveChanges();
            user = this.dbContext.CreateDetachedCopy(attachedUser);

            return Task.FromResult<Object>(null);
        }
    }
}
