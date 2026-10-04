namespace ITServiceManager.API.Dtos.Device
{
    public class DeviceQueryDto
    {
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public int? DeviceTypeId { get; set; }
        public int? CustomerId { get; set; }
    }
}
