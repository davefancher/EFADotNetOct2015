using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        // Create
        // Read
        // Update
        // Delete

        public IEnumerable<NoteListItemViewModel> GetAllForUser(Guid userId)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                return
                    ctx
                        .Notes
                        .Where(n => n.ApplicationUserId == userId)
                        .Select(
                            n => new NoteListItemViewModel
                            {
                                Id = n.Id,
                                Title = n.Title,
                                DateCreated = n.DateCreated,
                                DateModified = n.DateModified,
                                IsFavorite = n.IsFavorite
                            })
                        .ToArray();
            }
        }

        public bool Create(NoteDetailViewModel vm, Guid userId)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var note =
                    new Note
                    {
                        Title = vm.Title,
                        Contents = vm.Contents,
                        DateCreated = DateTime.UtcNow,
                        ApplicationUserId = userId,
                        IsFavorite = vm.IsFavorite
                    };

                ctx.Notes.Add(note);

                return ctx.SaveChanges() == 1;
            }
        }

        public NoteDetailViewModel GetById(int id, Guid userId)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                return
                    ctx
                        .Notes
                        .Where(
                            n => n.Id == id && n.ApplicationUserId == userId)
                        .Select(
                            n =>
                                new NoteDetailViewModel
                                {
                                    Id = n.Id,
                                    Title = n.Title,
                                    Contents = n.Contents,
                                    IsFavorite = n.IsFavorite
                                })
                        .SingleOrDefault();
            }
        }

        public bool Update(NoteDetailViewModel vm, Guid userId)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var note =
                    ctx
                        .Notes
                        .Where(
                            n => n.Id == vm.Id
                                 && n.ApplicationUserId == userId)
                        .SingleOrDefault();

                if (note == null) return false;

                note.Contents = vm.Contents;
                note.Title = vm.Title;
                note.IsFavorite = vm.IsFavorite;
                note.DateModified = DateTime.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool Delete(int id, Guid userId)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var note =
                    ctx
                        .Notes
                        .Where(
                            n => n.Id == id
                                 && n.ApplicationUserId == userId)
                        .SingleOrDefault();

                if (note == null) return false;

                ctx.Notes.Remove(note);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
