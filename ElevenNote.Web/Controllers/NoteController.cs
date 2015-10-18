using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly Lazy<Guid> _userId;

        public NoteController()
        {
            _userId =
                new Lazy<Guid>(
                    () => Guid.Parse(User.Identity.GetUserId())
                );
        }

        public ActionResult Index()
        {
            var svc = new NoteService();
            var notes = svc.GetAllForUser(_userId.Value);

            return View(notes);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult CreateGet()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(NoteDetailViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var svc = new NoteService();

            var result = svc.Create(vm, _userId.Value);

            TempData.Add(
                "Result",
                result
                    ? "Note added"
                    : "Note not added");

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Details")]
        public ActionResult DetailsGet(int id)
        {
            var svc = new NoteService();
            var note = svc.GetById(id, _userId.Value);

            return View(note);
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditGet(int id)
        {
            var svc = new NoteService();
            var note = svc.GetById(id, _userId.Value);

            return View(note);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(NoteDetailViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var svc = new NoteService();
            var result = svc.Update(vm, _userId.Value);

            TempData.Add(
                "Result",
                result ? "Note updated" : "Note not updated");

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var svc = new NoteService();
            var note = svc.GetById(id, _userId.Value);

            return View(note);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var svc = new NoteService();
            var result = svc.Delete(id, _userId.Value);

            TempData.Add(
                "Result",
                result
                    ? "Note deleted"
                    : "Note not deleted");

            return RedirectToAction("Index");
        }
    }
}