using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelBindDemo.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Addr { get; set; }
        public Role Role { get; set; }
    }

    public class Address
    {
        public string ZipCode { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public enum Role
    {
        Admin,
        User,
        Guest 
    }
}