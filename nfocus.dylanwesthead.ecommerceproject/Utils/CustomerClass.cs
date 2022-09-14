using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    internal class Customer
    {
        public string first;
        public string surname;
        public string address;
        public string town;
        public string postcode;
        public string phone;
        public string email;

        public Customer(string first, string surname, string address, string town, string postcode, string phone, string email)
        {
            this.first = first;
            this.surname = surname;
            this.address = address;
            this.town = town;
            this.postcode = postcode;
            this.phone = phone;
            this.email = email;
        }
    }
}
