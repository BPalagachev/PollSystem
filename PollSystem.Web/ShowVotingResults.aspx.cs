using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PollSystem.Web
{
    public partial class ShowVotingResults : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string questionId = Request.Params["questionId"];
            var currentQuestion = this.DbContext.Questions.FirstOrDefault(x => x.Id == questionId);
            this.ThrowExceptionIf(currentQuestion == null, new ArgumentException(string.Format("No answer with id {0} found", questionId)));
            this.RepeaterAnswers.DataSource = currentQuestion.Answers;
            this.RepeaterAnswers.DataBind();
            this.LabelQuestion.Text = currentQuestion.QueryText;

        }
    }
}