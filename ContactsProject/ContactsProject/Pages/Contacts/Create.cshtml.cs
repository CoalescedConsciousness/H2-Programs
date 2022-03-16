#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactList.Models;
using ContactsProject.Data;
using Microsoft.Data.SqlClient;

namespace ContactsProject.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly IRepository _repo;
        private readonly ContactsProject.Data.ContactsProjectContext _context;

        public CreateModel(ContactsProject.Data.ContactsProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Contact.Add(Contact);
            Repository.ContactCreate(Contact.Name, Contact.Email, Contact.Phone, Contact.IsFavourite);
            
            return RedirectToPage("./Index");
        }
    }
}
