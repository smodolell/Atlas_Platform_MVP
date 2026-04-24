namespace Atlas.Application.Features.Accounts.Usuarios.Commands;

public class DeleteUsuarioCommand : ICommand<Result>
{
    public required int UsuarioId { get; set; }
}

public class DeleteUsuarioCommandHandler : ICommandHandler<DeleteUsuarioCommand, Result>
{
    public Task<Result> HandleAsync(DeleteUsuarioCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            return Task.FromResult(Result.Error("Funcionalidad no implementada."));
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Error(ex.Message));
        }
    }
}
