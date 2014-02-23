using PollSystem.OpenAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PollSystem.Web
{
    public partial class EditAnswer : BasePage
    {
        private Question requestedQuestion;
        private Answer requestedAnswer;
        private IdentityUser requestedUser;

        protected string RequestedAnswerId
        {
            get
            {
                object idParameter = this.Request.Params["AnswerId"];
                this.ThrowExceptionIf(idParameter == null || string.IsNullOrEmpty(idParameter.ToString()),
                    new InvalidOperationException("Requested question answer id cannot be null."));

                return idParameter.ToString();
            }
        }

        protected Answer RequestedAnswer
        {
            get
            {
                if (this.requestedAnswer == null)
                {
                    this.requestedAnswer = this.DbContext.Answers.FirstOrDefault(x => x.Id == RequestedAnswerId);
                    this.ThrowExceptionIf(this.requestedAnswer == null, new ArgumentNullException("Cannot find an answer with the specified id."));
                }

                return this.requestedAnswer;
            }
        }

        protected Question RequestedQuestion
        {
            get 
            {
                if (this.requestedQuestion == null)
                {
                    this.requestedQuestion = this.RequestedAnswer.Question;
                    this.ThrowExceptionIf(this.requestedQuestion == null, new ArgumentNullException("Cannot find the question that the current answer belong to."));
                }

                return this.requestedQuestion;
            }
        }

        protected IdentityUser RequestedUser
        {
            get
            {
                if (this.requestedUser == null)
                {
                    this.requestedUser = this.RequestedQuestion.PostedBy;
                    this.ThrowExceptionIf(this.requestedUser == null, new ArgumentNullException("This answer does not belong to any user"));
                }

                return this.requestedUser;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ThrowExceptionIf(this.CurrentUser.Equals(this.RequestedUser) == false, new InvalidOperationException("You are not authorized to delete this question because another user posted it"));
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            this.LiteralBoxQueryText.Text = this.RequestedQuestion.QueryText;
            this.TextBoxAnswerText.Text = this.RequestedAnswer.AnswerText;
        }

        protected void LinkButtonSaveAnswerText_Click(object sender, EventArgs e)
        {
            var newAnswerText = this.TextBoxAnswerText.Text;
            this.ThrowExceptionIf(string.IsNullOrWhiteSpace(newAnswerText), new ArgumentException("Cannot insert empty answer"));
            this.RequestedAnswer.AnswerText = newAnswerText;
            this.DbContext.SaveChanges();

            this.Response.Redirect("~/EditQuestion?QuestionId="+this.requestedQuestion.Id);
        }

        protected void LinkButtonDeleteAnswer_Click(object sender, EventArgs e)
        {
            this.DbContext.Delete(this.RequestedAnswer);
            this.DbContext.SaveChanges();

            this.Response.Redirect("~/EditQuestion?QuestionId=" + this.requestedQuestion.Id);
        }

    }
}