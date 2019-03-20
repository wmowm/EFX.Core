using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.IService.Tibos;

namespace Tibos.Admin.Components
{
    public class PositionViewComponent:ViewComponent
    {

        private IDictTypeService _dictTypeService;
        private IDictService _dictService;
        public PositionViewComponent(IDictTypeService dictTypeService, IDictService dictService)
        {
            this._dictTypeService = dictTypeService;
            this._dictService = dictService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Manager request)
        {
            var m_dictType = await _dictTypeService.GetAsync(m => m.Mark == "Position");
            var list_dict = _dictService.GetList(m => m.Tid == m_dictType.Id && m.Status == 1);
            ViewBag.PositionId = request.PositionId;
            return View(list_dict);
        }
    }
}
