namespace ITServiceManager.API.Dtos.Device
{
    public class UpdateDeviceDto
    {
        public string Manufacturer { get; set; } = "";
        public string Model { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public int CustomerId { get; set; }
        public int DeviceTypeId { get; set; }
    }
}
