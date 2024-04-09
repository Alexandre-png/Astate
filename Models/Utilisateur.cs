using System.ComponentModel.DataAnnotations;

namespace Astate.Models;

public class Utilisateur
{

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom de famille est requis.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Le prénom est requis.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "L'adresse e-mail est requise.")]
    [EmailAddress(ErrorMessage = "L'adresse e-mail n'est pas au bon format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
    [RegularExpression(@"^(?=.*[!@#$%^&*])",
            ErrorMessage = "Le mot de passe doit contenir au moins un caractère spécial.")]
    public string Password { get; set; }
    
    // Propriété pour stocker le sel utilisé lors du hachage du mot de passe
    public byte[] Salt { get; set; }
}