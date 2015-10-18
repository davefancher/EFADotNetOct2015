using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.Web.Controllers.WebApi
{
    [Authorize]
    public class FavoritesController : ApiController
    {
        private readonly Lazy<Guid> _userId;

        public FavoritesController()
        {
            _userId =
                new Lazy<Guid>(() => Guid.Parse(User.Identity.GetUserId()));
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id)
        {
            var svc = new NoteService();

            var note = svc.GetById(id, _userId.Value);
            note.IsFavorite = true;

            var result = svc.Update(note, _userId.Value);

            return Ok(result);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var svc = new NoteService();

            var note = svc.GetById(id, _userId.Value);
            note.IsFavorite = false;

            var result = svc.Update(note, _userId.Value);

            return Ok(result);
        }
    }
}