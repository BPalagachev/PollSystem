using PollSystem.OpenAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;

namespace PollSystem.Web
{
    public partial class UserQuestions : BasePage
    {
        protected bool CurrestUserRequested
        {
            get { return this.CurrentUser.Id == this.Request["UserID"].ToString(); }
        }

        protected IdentityUser RequestedUser
        {
            get 
            {
                if (this.CurrestUserRequested)
                {
                    return this.CurrentUser;
                }
                else
                {
                    return this.DbContext.IdentityUsers.FirstOrDefault(x => x.Id == this.Request["UserID"].ToString());
                }
            }
        }

        private const int pageSize = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            RedirectUnauthorizedUsers();

            this.ThrowExceptionIf(this.Request["UserID"] == null || string.IsNullOrEmpty(this.Request["UserID"].ToString()), new ArgumentNullException("Requested user id cannot be null."));
            this.ThrowExceptionIf(this.RequestedUser == null, new InvalidOperationException("Cannot find the requested user"));      
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            this.GridViewQuestions.PageSize = pageSize;

            if (!this.IsPostBack)
            {
                this.GridViewQuestions_PageIndexChanging(this, new GridViewPageEventArgs(0));
            }

            this.LiteralCurrectUser.Text = this.RequestedUser.UserName;
            this.HideEditColumnIf(this.CurrentUser.Equals(this.RequestedUser) == false);
        }

        protected void GridViewQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var currentPage = e.NewPageIndex;
            var questions = this.RequestedUser
                .CreatedQuestions
                .Skip(pageSize * currentPage)
                .Take(pageSize * currentPage + pageSize)
                .ToList();
            this.GridViewQuestions.DataSource = questions;

            this.GridViewQuestions.PageIndex = e.NewPageIndex;
            this.GridViewQuestions.VirtualItemCount = this.DbContext.Questions.Count();

            this.DataBind();
        }

        private void HideEditColumnIf(bool condition)
        {
            if (condition)
            {
                this.GridViewQuestions.Columns[2].Visible = false;
            }
        }
    }
}