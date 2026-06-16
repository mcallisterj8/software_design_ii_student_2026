using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BugLab01.Models {
    public class PlaylistTrack {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
    }
}
