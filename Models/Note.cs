using System.ComponentModel.DataAnnotations;

namespace Astate.Models;

public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdOwner { get; set; }

        [Required]
        public int IdLivre { get; set; }

        [Required]
        public int IdTag { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }