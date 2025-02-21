namespace Identity.Application.Commands.Common;

public class UserResponseModel
{
    public UserResponseModel(string token) => Token = token;

    public string Token { get; }
}