using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Models
{
    public class zTree
    {
        public string id { get; set; }

        public string pId { get; set; }

        public string name { get; set; }

        public bool open { get; set; }

        public bool noRemoveBtn { get; set; }

        public bool noEditBtn { get; set; }
    }
}
