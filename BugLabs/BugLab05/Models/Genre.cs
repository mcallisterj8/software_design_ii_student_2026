using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BugLab05.Models {
    [DebuggerDisplay("{Name} (GenreId = {GenreId})")]
    public class Genre {
        [Key]
        public int GenreId { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
