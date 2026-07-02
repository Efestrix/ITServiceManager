namespace ITServiceManager.API.Dtos.RepairOrder
{
    public class RepairOrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int DeviceId { get; set; }
        public int TechnicianId { get; set; }
        public int StatusId { get; set; }
    }
}
