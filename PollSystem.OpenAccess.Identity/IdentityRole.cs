using Microsoft.AspNet.Identity;
using System;
using System.Linq;

namespace PollSystem.OpenAccess.Identity
{
    public class IdentityRole : IRole
    {
        public IdentityRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string name)
            :base()
        {
            this.Name = name;
        }

        public IdentityRole(string name, string id)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get; private set; }

        public string Name { get; set; }
    }
}
