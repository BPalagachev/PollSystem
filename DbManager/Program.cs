using PollSystem.OpenAccess.Identity;
using PollSystem.OpenAccess.Identity.DalHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PollSystemDbCotnext dbContext = new PollSystemDbCotnext();
            dbContext.CreateOrUpdateDatabase();
            SeedDb(dbContext);
        }

        private static void SeedDb(PollSystemDbCotnext dbContext)
        {
            dbContext.Add(CreateWhatsYourFavoriteProgrammingLanguage());
            dbContext.Add(CreateWhatsYourHomeTown());
            dbContext.Add(CreateWhatsYourFavoriteBeer());
            dbContext.Add(CreateWhatsYourFavoriteColor());

            dbContext.SaveChanges();
        }
        
        private static Question CreateWhatsYourFavoriteProgrammingLanguage()
        {
            var questionBuilder = new QuestionBuilder();
            var question = questionBuilder
                                .WithQueryText("What is your favorite programming lanquage?")
                                .AddMultipleAnswers("C#", "VB", "Java Script", "Java", "Python", "Ruby")
                                .BuildQuestion();

            return question;
        }

        private static Question CreateWhatsYourHomeTown()
        {
            var questionBuilder = new QuestionBuilder();
            var question = questionBuilder
                                .WithQueryText("What is your home town?")
                                .AddMultipleAnswers("Sofia", "Varna", "Burgas", "Plovdiv")
                                .BuildQuestion();

            return question;
        }

        private static Question CreateWhatsYourFavoriteBeer()
        {
            var questionBuilder = new QuestionBuilder();
            var question = questionBuilder
                                .WithQueryText("Whats is your favorite beer?")
                                .AddMultipleAnswers("Ariana", "Hoegaarden", "Tuborg", "Staropramen")
                                .BuildQuestion();

            return question;
        }

        private static Question CreateWhatsYourFavoriteColor()
        {
            var questionBuilder = new QuestionBuilder();
            var question = questionBuilder
                                .WithQueryText("Whats is your favorite color?")
                                .AddMultipleAnswers("Red", "Green", "Blue", "Alpha")
                                .BuildQuestion();

            return question;
        }
    }
}
