namespace ITServiceManager.API.Dtos.Photo
{
    public class CreatePhotoDto
    {
        public int RepairOrderId { get; set; }
        public string FileName { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
