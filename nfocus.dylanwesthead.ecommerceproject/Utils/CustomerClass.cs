/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - Customer object class to create and access the customer instances easily. 
 */
namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    internal class Customer
    {
        internal string _first;
        internal string _surname;
        internal string _address;
        internal string _town;
        internal string _postcode;
        internal string _phone;
        internal string _email;

        internal Customer(string first, string surname, string address, string town, string postcode, string phone, string email)
        {
            this._first = first;
            this._surname = surname;
            this._address = address;
            this._town = town;
            this._postcode = postcode;
            this._phone = phone;
            this._email = email;
        }
    }
}
