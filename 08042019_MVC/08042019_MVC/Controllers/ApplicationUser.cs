using System;
using _08042019_MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _08042019_MVC.Controllers
{
    internal class ApplicationUser<T>
    {
        private UserStore<ApplicationUser> userStore;

        public ApplicationUser(UserStore<ApplicationUser> userStore)
        {
            this.userStore = userStore;
        }

        internal object GetRoles(string v)
        {
            throw new NotImplementedException();
        }
    }
}