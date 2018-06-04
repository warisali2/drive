using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drive.Security
{
    public static class SessionManager
    {
        public static User User
        {
            get
            {
                User dto = null;

                if(HttpContext.Current.Session["User"] != null)
                {
                    dto = HttpContext.Current.Session["User"] as User;
                }

                return dto;
            }

            set
            {
                HttpContext.Current.Session["User"] = value;
            }
        }

        public static Boolean IsValidUser
        {
            get
            {
                if (User != null)
                    return true;
                else
                    return false;
            }
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }
    }
}