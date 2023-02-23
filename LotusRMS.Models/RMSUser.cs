

using Microsoft.AspNetCore.Identity;

namespace LotusRMS.Models
{
    public class RMSUser:IdentityUser
    {
        public string FirstName { get; private set; }

      
        public string? MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Contact { get; private set; }
        public RMSUser() { }
        public RMSUser(string firstName, string middleName, string lastName, string contact)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Contact = contact;

        }

        public void Update(string firstName, string middleName, string lastName, string contact)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Contact = contact;

        }

        
    }
}
