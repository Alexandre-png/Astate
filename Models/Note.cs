using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Astate.Models;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required(ErrorMessage = "L'ID du propriétaire est requis.")]
    public string IdOwner { get; set; }

    [ForeignKey("IdOwner")]
    public IdentityUser Owner { get; set; }

    [Required(ErrorMessage = "Le titre du livre est requis.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Le contenu de la note est requis.")]
    public string Content { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}

public class NoteDto
{
    public string IdOwner { get; set; }

    [Required(ErrorMessage = "Le titre du livre est requis.")]
    public string Title { get; set; }


    [Required(ErrorMessage = "Le contenu de la note est requis.")]
    public string Content { get; set; }

    public string? ImageUrl { get; set; }
}