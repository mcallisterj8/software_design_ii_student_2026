using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BugLab03.Models {
    [DebuggerDisplay("{Name} (PlaylistId = {PlaylistId})")]
    public class Playlist {
        [Key]
        public int PlaylistId { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }
        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
