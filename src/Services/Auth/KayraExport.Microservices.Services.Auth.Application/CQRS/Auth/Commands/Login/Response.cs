namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Login;

public class Response
{
    public TokenItem AccessToken { get; set; } = new();
    public TokenItem RefreshToken { get; set; } = new();
}

public class TokenItem
{
    public string Value { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}