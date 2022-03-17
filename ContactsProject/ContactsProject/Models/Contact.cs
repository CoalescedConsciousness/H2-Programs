using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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

        [Display(Name = "Last Edited")]
        [HiddenInput]
        public DateTime? EditDate { get; set; } 
        
        [Display(Name = "Created on")]
        [HiddenInput]
        public DateTime? CreateDate { get; set; }

        [HiddenInput]
        public bool Active { get; set; }

    }
}
