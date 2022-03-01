using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{

    public class Contact
    {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [BindProperty]
        public string Name { get; set; }

        [Display(Name = "Favourite")]
        [BindProperty]
        public bool IsFavourite { get; set; }


        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")] // I'm not deleting this after all that work, even if there's an attribute! 
        [BindProperty]
        public string Email { get; set; }

        [RegularExpression(@"[0-9]{8}")]
        [BindProperty]
        public int Phone { get; set; }

        [HiddenInput]
        public string? EditDate { get; set; } 

        [HiddenInput]
        public string? CreateDate { get; set; }

    }
}
