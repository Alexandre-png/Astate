using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Astate.Models;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required(ErrorMessage = "L'ID du propriétaire est requis.")]
    public string IdOwner { get; set; }

    [Required(ErrorMessage = "L'ID du livre est requis.")]
    public string IdLivre { get; set; }

    [Required(ErrorMessage = "Le titre du livre est requis.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Le contenu de la note est requis.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "L'URL de l'image est requise.")]
    public string ImageUrl { get; set; }
}

public class NoteDto
{
    public string IdOwner { get; set; }

    [Required(ErrorMessage = "L'ID du livre est requis.")]
    public string IdLivre { get; set; }

    [Required(ErrorMessage = "Le titre du livre est requis.")]
    public string Title { get; set; }


    [Required(ErrorMessage = "Le contenu de la note est requis.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "L'URL de l'image est requise.")]
    public string ImageUrl { get; set; }
}