#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactList.Models;

namespace ContactsProject.Data
{
    public class ContactsProjectContext : DbContext
    {
        public ContactsProjectContext (DbContextOptions<ContactsProjectContext> options)
            : base(options)
        {
        }

        public DbSet<ContactList.Models.Contact> Contact { get; set; }
    }
}
