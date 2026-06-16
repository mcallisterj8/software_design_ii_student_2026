using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace BugLab05.Models {
    [DebuggerDisplay("InvoiceLineId = {InvoiceLineId}")]
    public class InvoiceLine {
        [Key]
        public int InvoiceLineId { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        [ForeignKey("Track")]
        public int TrackId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Track Track { get; set; }
    }
}
