using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models.Login
{
    public class LoginState
    {
        blogContext blogContext = new blogContext();

        public bool IsLoginSucces(string username,string password)
        {
            User user = blogContext.User.Where(u => u.Name==username && u.Password==password).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Current.Session.Add("UserId", user.UserId.ToString());
                return true;
            }
            return false;
        }
    }
}