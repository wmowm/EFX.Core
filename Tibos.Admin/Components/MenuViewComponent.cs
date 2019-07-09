using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.IService.Tibos;

namespace Tibos.Admin.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private INavigationService _navigationService;


        public MenuViewComponent(INavigationService navigationIService)
        {
            this._navigationService = navigationIService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.Identity.Name;
            var list = _navigationService.GetList(userId);
            return View(list);
        }
    }
}
