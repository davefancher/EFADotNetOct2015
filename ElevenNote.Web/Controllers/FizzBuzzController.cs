using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    public class FizzBuzzController : Controller
    {
        string ToFizzBuzz(int value)
        {
            if (value % 3 == 0 && value % 5 == 0)
            {
                return "FizzBuzz";
            }

            if (value % 3 == 0)
            {
                return "Fizz";
            }

            if (value % 5 == 0)
            {
                return "Buzz";
            }

            return value.ToString();
        }

        public ActionResult LogicInView()
        {
            ViewBag.Message = "FizzBuzz: Logic in View";

            return View();
        }

        public ActionResult LogicInController()
        {
            ViewBag.Message = "FizzBuzz: Logic in Controller";

            var items =
                Enumerable
                    .Range(1, 100)
                    .Select(ToFizzBuzz);

            return View(items);
        }
    }
}