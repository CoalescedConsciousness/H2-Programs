#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactList.Models;
using ContactsProject.Data;

namespace ContactsProject.Pages.Contacts
{
    public class EditModel : PageModel
    {
        private readonly ContactsProject.Data.ContactsProjectContext _context;

        public EditModel(ContactsProject.Data.ContactsProjectContext context)
        {
            
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contact = await _context.Contact.FirstOrDefaultAsync(m => m.Id == id);

            if (Contact == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            //Contact.EditDate = DateTime.Now.ToString();
            _context.Attach(Contact).State = EntityState.Modified;
            


            try
            {
                //await _context.SaveChangesAsync();
                Repository.ContactWrite(Contact.Id, Contact.Name, Contact.Email, Contact.Phone, Contact.IsFavourite);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(Contact.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}
