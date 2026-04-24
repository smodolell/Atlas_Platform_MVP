using Atlas.ApiService.Infrastructure;
using Atlas.Application.Common.DTOs;
using Atlas.Application.Features.Auth.Commands;
using Atlas.Application.Features.Auth.DTOs;
using Atlas.Application.Features.Auth.Queries;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Auth : EndpointGroupBase
{
    public override string? GroupName => "auth";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Autenticación");

        #region Auth
        group.MapPost("login", Login)
            .WithName("Login")
            .WithSummary("Autenticación de usuario")
            .Accepts<LoginCommand>("application/json")
            .Produces<ApiResponseDto<UsuarioLoginDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();

        group.MapPost("validate-token", ValidateToken)
            .WithName("ValidateToken")
            .WithSummary("Valida un token JWT")
            .Produces<ApiResponseDto<TokenValidationDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
        #endregion

    }

    #region Auth
    public async Task<IResult> Login(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] LoginCommand model)
    {
        var result = await commandMediator.SendAsync(model);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> ValidateToken(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string token)
    {
        var result = await queryMediator.QueryAsync(new ValidateTokenQuery(token));
        return result.ToCustomMinimalApiResult();
    }
    #endregion

   
}
