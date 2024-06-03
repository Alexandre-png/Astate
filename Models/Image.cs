using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Astate.Models;
public class Image
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string FileName { get; set; }

    [Required]
    public string FilePath { get; set; }

    [Required]
    public string ContentType { get; set; }

    [Required]
    public string UploadedById { get; set; }

    [ForeignKey("UploadedById")]
    public IdentityUser UploadedBy { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
