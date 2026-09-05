using Abstractions.Photon.Attributes;

namespace Data.Photon.Models.Models
{
    public class Product
    {
        [PhotonPrimaryKey]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}