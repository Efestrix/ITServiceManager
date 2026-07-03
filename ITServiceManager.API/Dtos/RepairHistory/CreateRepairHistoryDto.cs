namespace ITServiceManager.API.Dtos.RepairHistory
{
    public class CreateRepairHistoryDto
    {
        public int RepairOrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; } = "";
        public decimal HoursWorked { get; set; }
    }
}
