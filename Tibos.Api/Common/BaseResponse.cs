using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSnapshots.Models
{
    public class BaseResponse
    {

        public int code { get; set; }

        public string msg { get; set; }

        public object data { get; set; }
    }
}