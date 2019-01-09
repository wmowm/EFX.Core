using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Test;

namespace Test.Docker
{
    public class model
    {
        public string phrase { get; set; }

        public int serial_number { get; set; }
    }

    public class ResponsTest
    {
        public int status { get; set; }

        public string msg { get; set; }

        public object ex { get; set; }

        public object data { get; set; }
    }
    public class Test
    {
        static string[] A = { "junior", "onion", "detail" };
        static string[] B = { "emerge", "weapon", "robust" };
        static string[] C = { "isolate", "volcano", "rally" };
        static string[] D = { "private", "detect", "become" };

        static string[] E = { "A", "B", "C", "D" };

        public static void MainTest()
        {

            var mm = HttpCommon.HttpGetAsync("http://www.cimicmeigagroup.com/index.php?c=content&a=show&id=147");

            return;
            List<string> list = new List<string>();

            List<List<model>> res = new List<List<model>>();
            var temp = "";

            for (int i = 0; i < 10000; i++)
            {
                while (temp.Length < 4)
                {
                    var e = new Random().Next(1, E.Length + 1);
                    if (!temp.Contains(E[e - 1]))
                    {
                        temp += E[e - 1];
                    }

                }
                if (!list.Contains(temp))
                {
                    list.Add(temp);
                }
                temp = "";
            }
            List<model> list_model = new List<model>();
            foreach (var item in list)
            {
                list_model = new List<model>();

                foreach (var it in item)
                {
                    if (it.ToString() == "A")
                    {
                        foreach (var a in A)
                        {
                            model m = new model();
                            m.phrase = a;
                            m.serial_number = list_model.Count + 1;
                            list_model.Add(m);
                        }
                    }
                    else if (it.ToString() == "B")
                    {
                        foreach (var a in B)
                        {
                            model m = new model();
                            m.phrase = a;
                            m.serial_number = list_model.Count + 1;
                            list_model.Add(m);
                        }
                    }
                    else if (it.ToString() == "C")
                    {
                        foreach (var a in C)
                        {
                            model m = new model();
                            m.phrase = a;
                            m.serial_number = list_model.Count + 1;
                            list_model.Add(m);
                        }
                    }
                    else
                    {
                        foreach (var a in D)
                        {
                            model m = new model();
                            m.phrase = a;
                            m.serial_number = list_model.Count + 1;
                            list_model.Add(m);
                        }
                    }

                }

                res.Add(list_model);
            }

            foreach (var item in res)
            {
                var json = JsonConvert.SerializeObject(item);
                var kkk = HttpCommon.HttpPost("https://api.idcw.io:6369/api/SecuritySettings/GetUserInfoByPhrase", json);
                var pp = JsonConvert.DeserializeObject<ResponsTest>(kkk);
                if (pp.status != 105)
                {
                    var zz = "";
                    foreach (var it in item)
                    {
                        zz += it.phrase + ",";
                    }
                }
            }
        }
    }
}
