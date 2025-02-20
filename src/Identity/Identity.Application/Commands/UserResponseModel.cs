namespace Identity.Application.Commands;

public class UserResponseModel
{
    public UserResponseModel(string token) => Token = token;

    public string Token { get; }
}