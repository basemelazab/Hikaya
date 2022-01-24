using Hikaya.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Hikaya.Helpers
{
    public class UserHelper
    {
        public static string GetCurrentUserId()
        {
          return HttpContext.Current.User.Identity.GetUserId();

        }

        public static string GetUserNameById(string userId)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(userId);
            if (user != null)
            {
                return user.UserName;
            }
            return "";
        }

    }
}