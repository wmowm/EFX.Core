using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Service.Contract;

namespace Tibos.Admin.Components
{
    public class SelectViewComponent:ViewComponent
    {


        public SelectViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
