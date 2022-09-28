namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    internal class Customer
    {
        public string First;
        public string Surname;
        public string Address;
        public string Town;
        public string Postcode;
        public string Phone;
        public string Email;

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
