using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PollSystem.OpenAccess.Identity;
using Telerik.OpenAccess;

namespace PollSystem.Web
{
    public partial class _Default : BasePage
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            var questions = this.DbContext
                .Questions
                .Include(x => x.Answers)
                .OrderBy(x => "sys_guid()".SQL<Guid>())
                .Take(3);

            this.ListViewPolls.DataSource = questions;
            this.DataBind();
        }

        protected void Vote_Command(object sender, CommandEventArgs e)
        {
            if (this.CurrentUser == null)
            {
                Response.Redirect("~/Account/Login");
                return;
            }
            var answerId = e.CommandArgument.ToString();
            var answer = this.DbContext.Answers.FirstOrDefault(x => x.Id == answerId);
            this.ThrowExceptionIf(answer == null, new ArgumentNullException("No such answer found.", "anserId"));

            bool hasUserAlreadyVotedOn = this.CurrentUser.VotedOnQuestions.Any(x=>x.Id == answer.Question.Id);
            this.ThrowExceptionIf(hasUserAlreadyVotedOn, new InvalidOperationException("You have already voted on this question"));

            this.CurrentUser.VotedOnQuestions.Add(answer.Question);
            answer.Votes++;

            this.DbContext.SaveChanges();

            Response.Redirect("ShowVotingResults.aspx?questionId=" + answer.Question.Id);
        }

    }
}