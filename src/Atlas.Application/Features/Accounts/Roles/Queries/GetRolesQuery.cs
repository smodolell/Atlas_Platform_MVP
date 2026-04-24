using Ardalis.Specification.EntityFrameworkCore;
using Atlas.Application.Features.Accounts.Roles.Specifications;
using Atlas.Shared.Accounts.Roles;
using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Accounts.Roles.Queries;

public class GetRolesQuery : IQuery<Result<PagedResultDto<RolListItemDto>>>
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

internal class GetRolesQueryHandler(
    RoleManager<Rol> roleManager,
    IPaginator paginator
) : IQueryHandler<GetRolesQuery, Result<PagedResultDto<RolListItemDto>>>
{
    private readonly RoleManager<Rol> _roleManager = roleManager;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<RolListItemDto>>> HandleAsync(GetRolesQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new RolSpec(request.SearchText);

            var data = _roleManager.Roles
                .WithSpecification(spec);

            var result = await _paginator.PaginateAsync<Rol, RolListItemDto>(data, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
