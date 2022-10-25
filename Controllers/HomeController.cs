using BitforIT_Task_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitforIT_Task_2.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(long field1, string field2, string field3, long field4)
        {
            bool checkPassed = false;
            bool check1 = CheckField1(field1, 0, 99999);
            bool check2 = CheckField2(field2);
            bool check3 = CheckField3(field3);
            bool check4 = CheckField4(field4, -99999, 0);


            if (check1 && check2 && check3 && check4) 
            {
                string result = field1.ToString() + field2 + field3 + field4.ToString();
                decimal withPvn = InputToDecimal(result);
                double pvn = PriceTax(withPvn);
                double withoutPvn = PriceWithoutPVN(pvn, withPvn);
                ViewBag.Pvn = pvn;
                ViewBag.WithoutPvn = withoutPvn;
                
                return View();
            }
            else
            {
                return View();
               
            }



            ViewData["field1"] = field1;
            return View();
        }

        private static decimal InputToDecimal(object field1)
        {
            decimal sum = 0;
            foreach (char ch in field1.ToString())
            {
                sum += (decimal)(ch)/100;
            }
            return sum;
   
        }

        private double PriceTax(decimal sum)
        {
            
            double taxPercentage = 21;
            double tax = (Decimal.ToDouble(sum) * 100) / (100 + taxPercentage);
            
            return tax;
        }
        private double PriceWithoutPVN(double tax, decimal sum)
        {
            double SumMinusPVN;
            
            SumMinusPVN = (Decimal.ToDouble(sum) - tax);
            return SumMinusPVN;
        }
        
        private bool CheckField2(string field2)
        {
            if (field2!=null && field2.Length <= 3)
            {
                foreach (char ch in field2)
                {
                    if (!char.IsUpper(ch))
                    {
                        ViewBag.Field2Message = $"{field2} case is wrong ({field2})";
                        return false;
                    }
                }
                return true;
            }

            else
            {
                ViewBag.Field2Message = $"{field2} length or case is wrong ({field2})";
                return false;
            }
            
        }
        private bool CheckField3(string field3)
        {
            if (field3 != null && field3.Length <= 3)
            {
                foreach (char ch in field3)
                {
                    if (!char.IsLower(ch))
                    {
                        ViewBag.Field3Message = $"{field3} case is wrong ({field3})";
                        return false;
                    }
                }
                return true;
            }

            else
            {
                ViewBag.Field3Message = $"{field3} length or case is wrong ({field3})";
                return false;
            }

        }

        private bool CheckField4(long field4, long lowRange, long highRange)
        {
            if (field4 < lowRange || field4 > highRange)
            {
                ViewBag.Field4Message = $"{field4} not in valid range ({lowRange} to {highRange})";
                return false;
            }

            return true;
        }

        private bool CheckField1(long field1, long lowRange, long highRange)
        {

            if (field1 < lowRange || field1 > highRange )
            {
                ViewBag.Field1Message = $"{field1} not in valid range ({lowRange} - {highRange})";
                return false;
               
                
            }

            return true;
     
        }
    }
}
