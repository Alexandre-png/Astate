using System.ComponentModel.DataAnnotations;

namespace Astate.Models;

public class Note
    {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "L'ID du propriétaire est requis.")]
    public int IdOwner { get; set; }

    [Required(ErrorMessage = "L'ID du livre est requis.")]
    public int IdLivre { get; set; }

    [Required(ErrorMessage = "L'ID du tag est requis.")]
    public int IdTag { get; set; }

    [Required(ErrorMessage = "Le contenu de la note est requis.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "L'URL de l'image est requise.")]
    [Url(ErrorMessage = "L'URL de l'image doit être une URL valide.")]
    public string ImageUrl { get; set; }
}