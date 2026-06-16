using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BugLab02.Models {
    [DebuggerDisplay("{Name} (ArtistId = {ArtistId})")]
    public class Artist {
        [Key]
        public int ArtistId { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }
        public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
