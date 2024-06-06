using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Astate.Models;

public class Image
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string ContentType { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public string UploadedById { get; set; }

    [ForeignKey("UploadedById")]
    public ApplicationUser UploadedBy { get; set; }
}
