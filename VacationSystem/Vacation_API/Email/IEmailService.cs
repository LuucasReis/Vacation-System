namespace Vacation_API.Email
{
    public interface IEmailService
    {
        bool Enviar(string email, string assunto, string mensagem);
    }
}
