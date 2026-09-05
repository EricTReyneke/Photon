using Abstractions.Photon.Attributes;

namespace Data.Photon.Models.Models
{
    public class OrderItem
    {
        [PhotonPrimaryKey]
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}