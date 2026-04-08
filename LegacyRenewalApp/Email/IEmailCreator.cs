namespace LegacyRenewalApp.Email;

public interface IEmailCreator
{
    public (string subject, string body) CreateEmail();
}