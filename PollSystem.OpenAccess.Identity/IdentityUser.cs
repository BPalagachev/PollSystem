using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace PollSystem.OpenAccess.Identity
{
    public class IdentityUser : IUser
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedQuestions = new List<Question>();
            this.VotedOnQuestions = new List<Question>();
        }

        public IdentityUser(string userName)
            :base()
        {
            this.UserName = userName;
        }


        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public IList<Question> CreatedQuestions { get; set; }

        public IList<Question> VotedOnQuestions { get; set; }
    }

}
