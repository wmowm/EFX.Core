using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Common
{
    [Serializable]
    public class Params
    {
        public string key { get; set; }

        public object value { get; set; }

        public Common.EnumBase.SearchType searchType { get; set; }
    }

    [Serializable]
    public class Paging
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }

    [Serializable]
    public class Sort
    {
        public string key { get; set; }

        public Common.EnumBase.OrderType searchType { get; set; }
    }



    [Serializable]
    public class RequestParams
    {
        private List<Params> _params = new List<Params>();
        public List<Params> Params
        {
            get { return _params; }
            set { _params = value; }
        }


        private List<Sort> _sort = new List<Sort>();
        public List<Sort> Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }


        private Paging _paging = new Paging();
        public Paging Paging
        {
            get { return _paging; }
            set { _paging = value; }
        }
    }
}
