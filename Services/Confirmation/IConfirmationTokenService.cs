namespace PersonalAccount.Services.Confirmation;

public interface IConfirmationTokenService
{
    Task<string> GenerateTokenAsync(int accountId);
    Task<bool> ValidateTokenAsync(int accountId, string token);
    Task<bool> HasAnyConfirmedTokenAsync(int accountId);
    Task<HashSet<int>> GetConfirmedAccountIdsAsync();
}