namespace ITServiceManager.API.Dtos.Photo
{
    public class UpdatePhotoDto
    {
        public int RepairOrderId { get; set; }
        public string FileName { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
