using System.Web.Mvc;

namespace LojaVirtual.UI.Web.Areas.Cliente
{
    public class ClienteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cliente";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cliente_default",
                "Cliente/{controller}/{action}/{id}",
                new { action = "Index", controller="Home", id = UrlParameter.Optional },
                namespaces: new[] { "LojaVirtual.UI.Web.Areas.Cliente.Controllers" }
            );
        }
    }
}