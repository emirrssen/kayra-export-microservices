using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Events;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Helpers;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Consts;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Enums;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Auth.Application.Helpers;
using KayraExport.Microservices.Services.Auth.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Auth.Domain.Entities;
using Rebus.Bus;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Register;

public class Handler(
    ITransactionService transactionService,
    IUserRepository userRepository,
    IBus bus
) : CommandHandlerBase<Command>
{
    public override async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await userRepository.GetByEmailOrUsernameAsync(request.Email, request.Username);
            if (existingUser is not null)
                return BadRequestResponse("Bu e-posta adresine veya kullanıcı adına sahip bir kullanıcı zaten sistemde kayıtlı");

            var passwordHash = PasswordHelper.HashPassword(request.Password);
            if (passwordHash == null)
                return BadRequestResponse("Şifre bilgisi şifrelenemedi");

            await transactionService.BeginTransactionAsync();

            User user = new();
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.CreatedAt = DateTimeHelper.GetNowByTurkiyeTimeZone();
            user.EmailAddress = request.Email;

            await userRepository.InsertAsync(user);

            var affectedRows = await transactionService.SaveChangesAsync();
            if (affectedRows != 1)
            {
                await transactionService.RollbackTransactionAsync();
                return BadRequestResponse("Kullanıcı kaydedilemedi");
            }

            await transactionService.CommitTransactionAsync();
            return CreatedResponse();
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

            return BadRequestResponse("Beklenmeyen bir hata meydana geldi");
        }
    }
}
