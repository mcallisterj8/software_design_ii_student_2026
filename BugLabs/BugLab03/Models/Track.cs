using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;


namespace BugLab03.Models {
    [DebuggerDisplay("{Name} (TrackId = {TrackId})")]
    public class Track {
        [Key]
        public int TrackId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        [ForeignKey("MediaType")]
        public int MediaTypeId { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        [MaxLength(220)]
        public string? Composer { get; set; }

        public int Milliseconds { get; set; }

        public int Bytes { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Album Album { get; set; }

        public virtual MediaType MediaType { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
