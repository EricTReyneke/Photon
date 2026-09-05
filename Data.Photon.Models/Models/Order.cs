using Abstractions.Photon.Attributes;

namespace Data.Photon.Models.Models
{
    public class Order
    {
        [PhotonPrimaryKey]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    }
}