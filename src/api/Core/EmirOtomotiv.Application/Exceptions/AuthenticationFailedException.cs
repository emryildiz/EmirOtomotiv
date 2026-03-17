namespace EmirOtomotiv.Core.Application.Exceptions
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException() : base("Kimlik doğrulama başarısız") { }
    }
}