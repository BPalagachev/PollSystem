using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PollSystem.OpenAccess.Identity;
using Microsoft.AspNet.Identity;

namespace PollSystem.Web.Models
{
    public class UserManager : UserManager<IdentityUser>
    {
        public UserManager()
            : base(new UserStore())
        {
            
        }       
    }
}