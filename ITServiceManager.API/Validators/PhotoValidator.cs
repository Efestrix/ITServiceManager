using ITServiceManager.API.Dtos.Photo;

namespace ITServiceManager.API.Validators
{
    public static class PhotoValidator
    {
        public static ValidationResult Validate(CreatePhotoDto dto)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(dto.FileName))
                result.AddError("File name is required.");

            if (dto.FileName.Length > 255)
                result.AddError("File name is too long.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                result.AddError("Description is required.");

            if (dto.Description.Length > 500)
                result.AddError("Description cannot be longer than 500 characters.");

            if (dto.RepairOrderId <= 0)
                result.AddError("Repair order must be selected.");

            return result;
        }

        public static ValidationResult Validate(UpdatePhotoDto dto)
        {
            return Validate(new CreatePhotoDto
            {
                FileName = dto.FileName,
                Description = dto.Description,
                RepairOrderId = dto.RepairOrderId
            });
        }
    }
}
