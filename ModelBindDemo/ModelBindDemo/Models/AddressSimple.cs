using System.Web.Mvc;

namespace ModelBindDemo.Models
{
    //[Bind(Include="City")]
    public class AddressSimple
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}