using EverywhereNotes.Contracts.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EverywhereNotes.Models.Entities
{
    public class Note : IEntity
    {
        public long Id { get; init; }

        public string Title { get; set; }

        public string Content { get; set; }
      
        public NoteColors Color { get; set; }

        public DateTime CreationDateTime { get; init; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool MovedToBin { get; set; } = false;

        public long userId { get; init; }

        [ForeignKey(nameof(userId))]
        public User User { get; set; }
    }
}
