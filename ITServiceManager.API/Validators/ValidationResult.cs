using System.ComponentModel;

namespace ITServiceManager.API.Validators
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; set; } = new();

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
