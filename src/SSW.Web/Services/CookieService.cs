﻿using SSW.Data.Entities;
using System.Web;
using System.Web.Security;

namespace SSW.Web.Services
{
    public class CookieService
    {
        public void SetAuthenticationToken(string name, bool isPersistent, int timeout)
        {
            var ticket = new FormsAuthenticationTicket(name, isPersistent, timeout);

            var cookieData = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}