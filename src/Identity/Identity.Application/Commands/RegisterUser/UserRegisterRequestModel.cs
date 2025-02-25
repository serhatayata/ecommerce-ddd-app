namespace Identity.Application.Commands.RegisterUser;

public class UserRegisterRequestModel
{
    public UserRegisterRequestModel(
    string email,
    string username,
    string firstName,
    string lastName,
    string password)
    {
        Email = email;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }

    public string Email { get; }
    public string Username { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Password { get; }
}