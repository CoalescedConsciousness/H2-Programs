namespace Blog.Models
{
    public class Contacts
    {
       
        private string _id;
        private string _surname;
        private string _firstname;
        private List<ContactMethod> _contactMethods;

        public string Id { get { return _id; } set { _id = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public string Firstname { get { return _firstname; } set { _firstname = value;} }
        public List<ContactMethod> ContactMethods { get { return _contactMethods; } set { _contactMethods = value; } }

        public Contacts()
        {

        }

    }
}
