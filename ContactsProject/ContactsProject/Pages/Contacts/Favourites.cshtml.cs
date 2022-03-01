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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace ContactsProject.Pages.Contacts
{

    public class FavouritesModel : PageModel
    {
        private readonly ContactsProject.Data.ContactsProjectContext _context;

        public FavouritesModel(ContactsProject.Data.ContactsProjectContext context)
        {
            _context = context;
        }


        // This could be made even more dynamic, is mail/phone was given their separate models to play in.
        public enum ContactType { mail, phone }

        //[BindProperty]
        //public List<SelectListItem> CT { get; set; } = new List<SelectListItem> { new SelectListItem { Text = "Mail", Value = "1" }, new SelectListItem { Text = "Phone", Value = "2" } };

        //[BindProperty(SupportsGet = true)]
        public ContactType? CType { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SelectedType { get; set; }

        public int Selection { get; set; }

        public IList<Contact> Contact { get;set; }

        public async Task OnGetAsync([FromRoute] string selectedtype)
        {
            //var selectedValue = Request.Form["CType"];
            Contact = await _context.Contact.ToListAsync();
            //if (selectedValue == "0")
            //    SelectedType = "mail";
            //else if (selectedValue == "1")
            //    SelectedType = "phone";


        }
        
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            var selectedValue = Request.Form["CType"];
            Contact = await _context.Contact.ToListAsync();
            if (selectedValue == "0") {
                ViewData.Add("Selection", "mail");
                TempData.Add("Selection", "mail");
            }
            else if (selectedValue == "1")
            { 
                ViewData.Add("Selection", "phone"); 
                TempData.Add("Selection", "phone");
            }
            else SelectedType = "";
            //Contact = GetList();
            //ViewData.Add(SelectedType, Contact);


            return RedirectToPage("./Favourites/", ViewData);
        }

        //private IList<Contact> GetList()
        //{
        //    Contact = _context.Contact.ToList();
        //    IList<Contact> list = new List<Contact>();
        //    foreach (var contact in Contact)
        //    {
        //        if (contact.IsFavourite)
        //        {
        //            list.Add(contact);
        //        }
        //    }
        //    Contact = list.ToList();

        //    return list;
        //}
    }
}
