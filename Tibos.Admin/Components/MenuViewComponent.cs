using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.Service.Contract;

namespace Tibos.Admin.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private NavigationIService _navigationIService;

        public MenuViewComponent(NavigationIService navigationIService)
        {
            this._navigationIService = navigationIService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            NavigationRequest request = new NavigationRequest();
            request.sortKey = "Sort";
            request.sortType = 0;
            var list = _navigationIService.GetList(request);
            return View(list);
        }
    }
}
