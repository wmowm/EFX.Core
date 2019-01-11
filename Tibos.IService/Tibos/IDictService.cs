using System;
using System.Collections.Generic;
using System.Text;
using Tibos.Domain;

namespace Tibos.IService.Tibos
{
    public interface IDictService:IBaseService<Dict>
    {
        string GetTest();
    }
}
