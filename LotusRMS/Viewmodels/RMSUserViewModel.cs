using Microsoft.AspNetCore.Identity;

namespace LotusRMS.Viewmodels
{
    public class RMSUserViewModel : IdentityUser
    {
        public string ReturnUrl { get; set; }
        public string FirstName { get; private set; }


        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Contact { get; private set; }
        public RMSUserViewModel(string returnUrl, string firstName, string middleName, string lastName, string contact)
        {
            ReturnUrl = returnUrl;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Contact = contact;

        }



    }
}
