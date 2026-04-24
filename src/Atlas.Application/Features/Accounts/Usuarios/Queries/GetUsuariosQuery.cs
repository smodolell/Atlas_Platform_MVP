
using Atlas.Application.Features.Accounts.Usuarios.Specifications;
using Atlas.Shared.Accounts.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Accounts.Usuarios.Queries;

public class GetUsuariosQuery : IQuery<Result<PagedResultDto<UsuarioListItemDto>>>
{
    private int _page = 1;
    private int _pageSize = 10;

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            < 1 => 10,
            > 100 => 100,
            _ => value
        };
    }

    public string? SearchText { get; set; }
}

internal class GetUsuariosQueryHandler(
    UserManager<Usuario> userManager,
    IPaginator paginator
) : IQueryHandler<GetUsuariosQuery, Result<PagedResultDto<UsuarioListItemDto>>>
{
    private readonly UserManager<Usuario> _userManager = userManager;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<UsuarioListItemDto>>> HandleAsync(GetUsuariosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new UsuarioBySearchTextSpec(request.SearchText);

            var data = _userManager.Users.WithSpecification(spec);


            var result = await _paginator.PaginateAsync<Usuario, UsuarioListItemDto>(data, request.Page, request.PageSize);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
