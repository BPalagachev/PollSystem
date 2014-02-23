using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollSystem.OpenAccess.Identity.DalHelpers
{
    public class QuestionBuilder
    {
        private Question currentQuestion;
        public QuestionBuilder()
        {
            this.currentQuestion = new Question();
        }

        public QuestionBuilder WithQueryText(string queryText)
        {
            this.currentQuestion.QueryText = queryText;
            return this;
        }

        public QuestionBuilder AddAnswer(string answerText)
        {
            var newAnswer = new Answer();
            newAnswer.AnswerText = answerText;
            this.currentQuestion.Answers.Add(newAnswer);

            return this;
        }

        public QuestionBuilder AddMultipleAnswers(params string[] answerTexts)
        {
            foreach (var answerStr in answerTexts)
            {
                this.AddAnswer(answerStr);
            }

            return this;
        }

        public QuestionBuilder WithUser(IdentityUser identityUser)
        {
            this.currentQuestion.PostedBy = identityUser;
            return this;
        }

        public Question BuildQuestion()
        {
            return this.currentQuestion;
        }
    }
}
