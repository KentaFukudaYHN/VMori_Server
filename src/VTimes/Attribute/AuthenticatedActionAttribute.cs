using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using VMori.Controllers;

namespace VMori.Attribute
{
    public class AuthenticatedActionAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AuthenticatedActionAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var targetController = context.Controller as VMoriBaseController;

            if (targetController.ADC != null)
                return;

            var claimPricial = context.HttpContext.User;
            var id = claimPricial.FindFirst(ClaimTypes.NameIdentifier);
            if (id == null)
                return;

            targetController.ADC = new ApplicationDataContainer()
            {
                LoginUser = new LoginUser()
                {
                    Id = id.Value,
                    Name = claimPricial.FindFirst(ClaimTypes.Name).Value,
                    Mail = claimPricial.FindFirst("Mail").Value,
                }
            };
        }
    }
}
