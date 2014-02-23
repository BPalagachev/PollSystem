using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;

namespace PollSystem.Web
{
    public partial class EditQuestion : BasePage
    {
        protected string RequestedQuestionId
        {
            get
            {
                object idParameter = this.Request.Params["QuestionId"];
                this.ThrowExceptionIf(idParameter == null || string.IsNullOrEmpty(idParameter.ToString()),
                    new InvalidOperationException("Requested question id cannot be null."));

                return idParameter.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RedirectUnauthorizedUsers();
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            var doesQuestionBelogToCurrentUser = this.CurrentUser.CreatedQuestions.Any(x => x.Id == this.RequestedQuestionId);
            this.ThrowExceptionIf(doesQuestionBelogToCurrentUser == false, new InvalidOperationException("This question has not been created by the current user"));

            var question = this.DbContext.Questions.Include(x => x.Answers).FirstOrDefault(x => x.Id == this.RequestedQuestionId);
            this.TextBoxQueryText.Text = question.QueryText;
            this.RepeaterAnswers.DataSource = question.Answers;
            this.RepeaterAnswers.DataBind();

        }

        protected void LinkButtonSaveQuestionText_Click(object sender, EventArgs e)
        {
            var newQuestionText = this.TextBoxQueryText.Text;
            this.ThrowExceptionIf(string.IsNullOrEmpty(newQuestionText), new ArgumentNullException("The quesry text cannot be null or empty string"));

            var question = this.DbContext.Questions.FirstOrDefault(x => x.Id == this.RequestedQuestionId);
            question.QueryText = newQuestionText;
            this.DbContext.SaveChanges();

            this.Response.Redirect("UserQuestions?UserId=" + question.PostedBy.Id);
        }

        protected void LinkButtonDeleteAnswer_Command(object sender, CommandEventArgs e)
        {
            var answer = DbContext.Answers.FirstOrDefault(x => x.Id == e.CommandArgument.ToString());
            DbContext.Delete(answer);
            DbContext.SaveChanges();
        }

    }
}