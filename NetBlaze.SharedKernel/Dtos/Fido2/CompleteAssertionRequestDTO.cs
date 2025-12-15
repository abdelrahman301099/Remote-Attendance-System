namespace NetBlaze.SharedKernel.Dtos.Fido2
{
    public class CompleteAssertionRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string AssertionJson { get; set; } = string.Empty;
    }
}
