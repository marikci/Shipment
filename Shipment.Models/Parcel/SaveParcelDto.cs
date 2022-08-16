namespace Shipment.Models.Parcel
{
    public class SaveParcelDto
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Value { get; set; }
    }
}
