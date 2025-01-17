﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Models
{
    public class LoginControl:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutedContext filterContext)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["UserID"].ToString()))
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/User/Login");
                }
 
    }
            catch (Exception)
            {

                HttpContext.Current.Response.Redirect("~/User/Login");
            }
        }
    }
}
