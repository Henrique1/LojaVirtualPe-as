using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using LojaVirtual.Dominio;
using Microsoft.Owin.Infrastructure;

namespace LojaVirtual.UI.Web.Helpers
{
    public static class Seguranca
    {
        public static void SignInCookie(Usuario usuario)
        {
            var identity = ClaimsIdentity(usuario, DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
        }

        public static dynamic SignInBearer(Usuario usuario)
        {

            var identity = ClaimsIdentity(usuario, Startup.OAuthBearerOptions.AuthenticationType);

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));
            var token = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            return new
            {
                usuario = new
                    {
                        usuario.Id,
                        usuario.Nome,
                        usuario.Permissoes
                    },
                token
            };
        }

        private static ClaimsIdentity ClaimsIdentity(Usuario usuario, string authenticationType)
        {
            var claims = new List<Claim>
            {
                new Claim("Nome", usuario.Nome),
                new Claim("Email", usuario.Email),
                new Claim("Id", usuario.Id.ToString()),

            };

            string[] permissaoArray = usuario.Permissoes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


            claims.AddRange(permissaoArray.Select(permissao => new Claim(ClaimTypes.Role, permissao)));

            var identity = new ClaimsIdentity(claims, authenticationType);
            return identity;
        }

        public static void SignOut()
        {
            HttpContext.Current.Request.GetOwinContext().Authentication.SignOut();
        }

        public static bool UserIsInRole(string role)
        {
            var ctx = (OwinContext)HttpContext.Current.Request.GetOwinContext();
            return ctx.Authentication.User.IsInRole(role);
        }

        public static Usuario GetUser()
        {
            var ctx = (OwinContext)HttpContext.Current.Request.GetOwinContext();
            var user = ctx.Authentication.User;

            var usuario = new Usuario
            {
                Nome = GetUserValue("Nome"),
                Email = GetUserValue("Email"),
                Id = int.Parse(GetUserValue("Id")),
            };

            var permissoes = new List<string>();

            foreach (var claim in user.Claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    permissoes.Add(claim.Value);
                }
            }

            usuario.Permissoes = permissoes.ToArray().ToString();

            return usuario;
        }

        public static string GetUserId()
        {
            return GetUserValue("Id");
        }
        private static string GetUserValue(string key)
        {
            var ctx = (OwinContext)HttpContext.Current.Request.GetOwinContext();
            var user = ctx.Authentication.User;
            if (user == null)
                return string.Empty;

            return user.FindFirst(key) == null ? string.Empty : user.FindFirst(key).Value;
        }
    }
}