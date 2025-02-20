namespace Identity.Application.Commands.ChangePassword;

public class ChangePasswordRequestModel
{
    public ChangePasswordRequestModel(
        int userId,
        string currentPassword,
        string newPassword)
    {
        UserId = userId;
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
    }

    public int UserId { get; }

    public string CurrentPassword { get; }

    public string NewPassword { get; }
}