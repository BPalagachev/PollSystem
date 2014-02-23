using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PollSystem.OpenAccess.Identity;

namespace PollSystem.Web.Models
{
    public class ContextFactory : IDisposable
    {
        private static readonly string contextKey = typeof(PollSystemDbCotnext).FullName;

        public static PollSystemDbCotnext GetCotnextPerRequest()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return new PollSystemDbCotnext();
            }
            else 
            {
                PollSystemDbCotnext dbContext = httpContext.Items[contextKey] as PollSystemDbCotnext;

                if (dbContext == null)
                {
                    dbContext = new PollSystemDbCotnext();
                    httpContext.Items[contextKey] = dbContext; 
                }

                return dbContext;
            }

        }

        public void Dispose()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                var currentContext = httpContext.Items[contextKey] as PollSystemDbCotnext;
                if (currentContext != null)
                {
                    currentContext.Dispose();
                    httpContext.Items[contextKey] = null;
                }
            }
        }
    }
}