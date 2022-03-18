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
    public class DetailsModel : PageModel
    {
        private Repository _context = null;

        public DetailsModel(ContactsProject.Data.Repository context)
        {
            this._context = context;
        }

        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Contact = await _context.Contact.FirstOrDefaultAsync(m => m.Id == id);
            Contact = _context.GetContactByID(id);
            _context.GetFields(Contact);

            if (Contact == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
