using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Events;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Helpers;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Consts;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Enums;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Auth.Application.Helpers;
using KayraExport.Microservices.Services.Auth.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Auth.Application.Services.Abstracts;
using Rebus.Bus;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Refresh;

public class Handler(
    ITransactionService transactionService,
    IUserRepository userRepository,
    ITokenService tokenService,
    IBus bus
) : CommandHandlerBase<Command, Response>
{
    public override async Task<DataResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
    {

        try
        {
            var user = await userRepository.GetByRefreshTokenAsync(request.RefreshToken);
            if (user is null)
                return BadRequestResponse("Kullanıcı bilgilerine ulaşılamadı.");

            if (user.RefreshTokenExpiresAt < DateTimeHelper.GetNowByTurkiyeTimeZone())
                return BadRequestResponse("Oturumunuzun süresi dolmuş. Lütfen yeniden giriş yapınız.");

            var tokenResult = await tokenService.CreateTokenAsync(user);
            if (tokenResult is null)
                return BadRequestResponse("Oturum anahtarı oluşturulamadı. Lütfen tekrar giriş yapınız");

            await transactionService.BeginTransactionAsync();

            user.RefreshToken = tokenResult.RefreshToken;
            user.RefreshTokenExpiresAt = tokenResult.RefreshTokenExpiration;
            await userRepository.UpdateAsync(user);

            var affectedRows = await transactionService.SaveChangesAsync();
            if (affectedRows != 1)
            {
                await transactionService.RollbackTransactionAsync();
                return BadRequestResponse("Güncellenen kayıt sayısı beklenenden farklı.");
            }

            await transactionService.CommitTransactionAsync();
            return OkResponse(new Response
            {
                AccessToken = new() { Value = tokenResult.AccessToken, ExpiresAt = tokenResult.RefreshTokenExpiration },
                RefreshToken = new() { Value = tokenResult.RefreshToken, ExpiresAt = tokenResult.RefreshTokenExpiration }
            });
        }
        catch (Exception ex)
        {
            await bus.Send(new LogMessageEvent
            {
                CreatedAt = DateTimeHelper.GetNowByTurkiyeTimeZone(),
                ExceptionDetails = ex.Message,
                Message = "Oturum yenilenirken beklenmeyen bir hata meydana geldi",
                LogLevel = LogLevelEnum.Error,
                ServiceName = LogServiceNameConst.AuthService
            });            

            return BadRequestResponse("Oturum yenilenirken beklenmeyen bir hata meydana geldi.");
        }
    }
}
