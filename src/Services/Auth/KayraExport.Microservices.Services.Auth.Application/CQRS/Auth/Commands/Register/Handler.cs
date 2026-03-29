using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Helpers;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Auth.Application.Helpers;
using KayraExport.Microservices.Services.Auth.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Auth.Domain.Entities;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Register;

public class Handler(
    ITransactionService transactionService,
    IUserRepository userRepository
) : CommandHandlerBase<Command>
{
    public override async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
    {
        bool isCommitted = false;

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
                return BadRequestResponse("Kullanıcı kaydedilemedi");

            await transactionService.CommitTransactionAsync();
            isCommitted = true;

            return CreatedResponse();
        }
        catch (Exception ex)
        {
            // TODO -> Bu kısımdaki exception'ı logla.
            return BadRequestResponse("Beklenmeyen bir hata meydana geldi");
        }
        finally
        {
            if (!isCommitted) await transactionService.RollbackTransactionAsync();
        }
    }
}
