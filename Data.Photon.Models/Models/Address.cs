using Abstractions.Photon.Attributes;

namespace Data.Photon.Models.Models
{
    public class Address
    {
        [PhotonPrimaryKey]
        public int AddressId { get; set; }

        public int CustomerId { get; set; }

        public string AddressLine1 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}