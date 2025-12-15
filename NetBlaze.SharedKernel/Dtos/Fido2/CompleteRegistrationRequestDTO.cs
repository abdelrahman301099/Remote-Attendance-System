namespace NetBlaze.SharedKernel.Dtos.Fido2
{
    public class CompleteRegistrationRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string AttestationJson { get; set; } = string.Empty;
    }
}
