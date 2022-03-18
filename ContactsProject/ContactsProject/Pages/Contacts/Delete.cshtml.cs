#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactList.Models;
using ContactsProject.Data;

namespace ContactsProject.Pages.Contacts
{
    public class DeleteModel : PageModel
    {
        private Repository _context = null;

        public DeleteModel(ContactsProject.Data.Repository context)
        {
            this._context = context;
        }


        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Contact = await _context.Contact.FirstOrDefaultAsync(m => m.Id == id);
            Contact = _context.GetContactByID(id);

            if (Contact == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contact = _context.GetContactByID(id);

            if (Contact != null)
            {
                //_context.Contact.Remove(Contact);
                //await _context.SaveChangesAsync();
                _context.ContactDelete(Contact.Id);

            }

            return RedirectToPage("./Index");
        }
    }
}
