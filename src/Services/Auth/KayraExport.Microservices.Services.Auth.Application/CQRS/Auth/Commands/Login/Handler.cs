using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Auth.Application.Helpers;
using KayraExport.Microservices.Services.Auth.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Auth.Application.Services.Abstracts;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Login;

public class Handler(
    ITransactionService transactionService,
    IUserRepository userRepository,
    ITokenService tokenService
) : CommandHandlerBase<Command, Response>
{
    public override async Task<DataResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetByEmailOrUsernameAsync(
                request.LoginCredentials,
                request.LoginCredentials
            );
            if (user is null)
                return BadRequestResponse("Girdiğiniz bilgiler hatalı. Lütfen tekrar deneyin.");

            bool isPasswordValid = PasswordHelper.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                return BadRequestResponse("Şifreniz hatalı");

            var tokenResult = await tokenService.CreateTokenAsync(user);
            if (tokenResult is null)
                return BadRequestResponse("Giriş başarısız. Lütfen tekrar deneyiniz");

            await transactionService.BeginTransactionAsync();

            user.RefreshToken = tokenResult.RefreshToken;
            user.RefreshTokenExpiresAt = tokenResult.RefreshTokenExpiration;

            await userRepository.UpdateAsync(user);
            var affectedRows = await transactionService.SaveChangesAsync();
            if (affectedRows != 1)
            {
                // TODO -> Bu kısmı logla.
                await transactionService.RollbackTransactionAsync();
                return BadRequestResponse("Giriş başarısız. Lütfen tekrar deneyiniz");
            }

            await transactionService.CommitTransactionAsync();

            return OkResponse(new Response()
            {
                AccessToken = new() { Value = tokenResult.AccessToken, ExpiresAt = tokenResult.RefreshTokenExpiration },
                RefreshToken = new() { Value = tokenResult.RefreshToken, ExpiresAt = tokenResult.RefreshTokenExpiration }
            });
        }
        catch (Exception ex)
        {
            // TODO -> Bu kısmı logla.
            return BadRequestResponse("Giriş işlemi sırasında beklenmeyen bir hata oluştu.");
        }
    }
}
