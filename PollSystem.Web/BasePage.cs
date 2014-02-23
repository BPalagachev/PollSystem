using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PollSystem.OpenAccess.Identity;
using PollSystem.Web.Models;
using System.Reflection.Emit;

namespace PollSystem.Web
{
    public class BasePage : Page
    {
        private IdentityUser currentUser;

        protected PollSystemDbCotnext DbContext { get; set; }

        protected IdentityUser CurrentUser 
        {
            get
            {
                if (this.currentUser == null)
                {
                    this.currentUser = LoadCurrentUser();
                }

                return this.currentUser;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DbContext = ContextFactory.GetCotnextPerRequest();
        }

        protected virtual void RedirectUnauthorizedUsers(string url  = "~/Account/Login")
        {
            if (this.CurrentUser == null)
            {
                Response.Redirect(url);
            }
        }

        protected void ThrowExceptionIf(bool condition, Exception exception) 
        {
            if (condition)
            {
                throw exception;                
            }
        }

        private IdentityUser LoadCurrentUser()
        {
            var username = HttpContext.Current.User.Identity.Name;
            var user = this.DbContext.IdentityUsers.SingleOrDefault(x => x.UserName == username);
            return user;
        }
    }
}