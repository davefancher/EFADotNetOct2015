using ElevenNote.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    public class GuessingGameController : Controller
    {
        private static readonly Random rand = new Random();

        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            var correctAnswer = rand.Next(1, 10);
            Session["Answer"] = correctAnswer;

            return View();
        }

        private bool GuessWasCorrect(int guess)
        {
            var correctAnswer = (int)Session["Answer"];
            return guess == correctAnswer;
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(GuessingGameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Win = GuessWasCorrect(vm.Guess);
            }

            return View(vm);
        }
    }
}
