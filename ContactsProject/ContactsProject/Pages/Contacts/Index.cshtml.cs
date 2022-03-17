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
    public class IndexModel : PageModel
    {
        private readonly ContactsProject.Data.ContactsProjectContext _context;

        public IndexModel(ContactsProject.Data.ContactsProjectContext context)
        {
            _context = context;
        }

        
        [BindProperty]  
        public List<Contact> Contact { get;set; }

        public async Task OnGetAsync()
        {
            //Contact = await _context.Contact.ToListAsync();
            Contact = await Repository.ContactGetAllAsync();
            Contact = Contact.OrderByDescending(x => x.IsFavourite).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            for (int i = 0; i < Contact.Count; i++)
            {
                //    //int id = Contact[i].Id;
                //    //Contact target = Contact.FirstOrDefault(x => x.Id == id);
                //    //target.IsFavourite = Request.Form["IsFavourite"].ToString() == "true" ? true : false;
                _context.Attach(Contact[i]).State = EntityState.Modified;
            }
            ////foreach (var contact in Contact)
            //{
            //    _context.Attach(contact).State = EntityState.Modified;
            //}


            _context.SaveChanges();
            Contact = Contact.OrderBy(x => x.IsFavourite).ToList();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Contact contact = new Contact();
            contact = Repository.GetContactByID(id);

            if (contact != null)
            {
                //_context.Contact.Remove(Contact);
                //await _context.SaveChangesAsync();
                Repository.ContactDelete(contact.Id);

            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostRestoreAsync(int id)
        {
            if (id == null) return NotFound();

            Contact contact = new Contact();
            contact = Repository.GetContactByID(id);

            if (contact != null)
            {
                Repository.ContactRestore(id);
            }
            return RedirectToPage("./Index");
        }
    }
}
