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
        private Repository _context = null;

        public EditModel(ContactsProject.Data.Repository context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            //Contact.EditDate = DateTime.Now.ToString();
            //_context.Save();

            try
            {
                //await _context.SaveChangesAsync();
                //_context.ContactWrite(Contact.Id, Contact.Name, Contact.Email, Contact.Phone, Contact.IsFavourite);
                _context.Update(Contact);
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
            return _context.GetContactByID(id) != null ? true : false;
        }
    }
}
