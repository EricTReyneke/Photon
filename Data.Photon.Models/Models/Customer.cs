using Abstractions.Photon.Attributes;

namespace Data.Photon.Models.Models
{
    public class Customer
    {
        [PhotonPrimaryKey]
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}