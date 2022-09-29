/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
 *
 *   - The customer object class to create and save the needed properties easily. 
 */
namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    internal class Customer
    {
        internal string First;
        internal string Surname;
        internal string Address;
        internal string Town;
        internal string Postcode;
        internal string Phone;
        internal string Email;

        /* The Customer Constructor
         * Holds the information and logic required to create a customer object for the Edgwords eCommerce site.
         */
        public Customer(string first, string surname, string address, string town, string postcode, string phone, string email)
        {
            this.First = first;
            this.Surname = surname;
            this.Address = address;
            this.Town = town;
            this.Postcode = postcode;
            this.Phone = phone;
            this.Email = email;
        }
    }
}
