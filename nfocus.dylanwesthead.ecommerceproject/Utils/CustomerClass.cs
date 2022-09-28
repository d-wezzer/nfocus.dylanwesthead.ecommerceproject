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

        public Customer(string First, string Surname, string Address, string Town, string Postcode, string Phone, string Email)
        {
            this.First = First;
            this.Surname = Surname;
            this.Address = Address;
            this.Town = Town;
            this.Postcode = Postcode;
            this.Phone = Phone;
            this.Email = Email;
        }
    }
}
