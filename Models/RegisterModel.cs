using System.ComponentModel.DataAnnotations;

namespace Astate.Models;
public class RegisterModel
{
    [Required(ErrorMessage = "L'adresse e-mail est requise.")]
    [EmailAddress(ErrorMessage = "L'adresse e-mail n'est pas au bon format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$",
            ErrorMessage = "Le mot de passe doit contenir au moins un chiffre, une majuscule, une minuscule et un caractère spécial.")]
    public string Password { get; set; }
}