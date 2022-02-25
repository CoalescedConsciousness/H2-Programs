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

        [BindProperty(SupportsGet = true)]
        public bool IsFavourite { get; set; } = false;


        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
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
