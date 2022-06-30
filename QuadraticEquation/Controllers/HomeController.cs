using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace DateTimeChecker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxMethod(string a, string b, string c)
        {
            Double num = 0;
            String text = null;
            if (a == null || b == null || c == null)
            {
                text = "<h1 class=\"text-danger\">Vui lòng điền đầy đủ</h1>";
            }
            else if(!Double.TryParse(a, out num) || !Double.TryParse(b, out num) || !Double.TryParse(c, out num))
            {
                text = "<h1 class=\"text-danger\">Vui lòng nhập số</h1>";
            }
            else
            {
                double soa = Convert.ToDouble(a);
                double sob = Convert.ToDouble(b);
                double soc = Convert.ToDouble(c);
                double x1, x2, delta;
                if (soa == 0)
                {
                    //ta giải phương trình bậc nhất bx + c = 0
                    if (sob == 0)
                    {
                        //nếu b = 0 và c = 0 thì phương trình vô số nghiệm
                        if (soc == 0)
                        {
                            text = "<h1 class=\"text-success\">Phương trình có vô số nghiệm</h1>";
                        }
                        //nếu b = 0 và c != 0 thì phương trình vô nghiệm
                        else
                        {
                            text = "<h1 class=\"text-success\">Phương trình vô nghiệm</h1>";
                        }
                    }
                    else
                    {
                        x1 = -soc / sob;// -soc/sob;    
                        text = "<h1 class=\"text-success\">Phương trình có nghiệm duy nhất x1 = " + x1 + "</h1>";
                    }
                }
                //nếu a != 0 thì ta bắt đầu giải phương trình bậc hai
                else
                {
                    //tính delta
                    delta = Math.Pow(sob, 2) - 4 * soa * soc;
                    //kiểm tra nếu delta < 0 thì phương trình vô nghiệm
                    if (delta < 0)
                    {
                        text = "<h1 class=\"text-success\">Phương trình vô nghiệm</h1>";
                    }
                    //nếu delta = 0 thì phương trình có hai nghiệm kép
                    else if (delta == 0)
                    {
                        x1 = x2 = -sob / (2 * soa);//-sob / (2 * soa);
                        text = "<h1 class=\"text-success\">Phương trình có nghiệm kép x1 = x2 = " + x1 + "</h1>";
                    }
                    //nếu delta > 0 thì phuong trình có hai nghiệm phân biệt
                    else
                    {
                        x1 = (-sob + Math.Sqrt(delta)) / (2 * soa);
                        x2 = (-sob - Math.Sqrt(delta)) / (2 * soa);
                        text = "<h1 class=\"text-success\">Phương trình có hai nghiệm phân biệt x1 = " + x1 +"; x2 = " + x2 + "</h1>";
                    }
                }
            }
            return Json(text);
        }
    }
}
