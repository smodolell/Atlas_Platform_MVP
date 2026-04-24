using Atlas.Application.Features.Auth.DTOs;
using Atlas.Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Auth.Commands;

//public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<UsuarioLoginDto>>
//{
//    private readonly IApplicationDbContext _context;
//    private readonly IMapper _mapper;
//    private readonly IJwtService _jwtService;

//    public LoginCommandHandler(IApplicationDbContext context, IMapper mapper, IJwtService jwtService)
//    {
//        _context = context;
//        _mapper = mapper;
//        _jwtService = jwtService;
//    }


//    public async Task<Result<UsuarioLoginDto>> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
//    {
//        var usuario = await _context.Usuarios
//            .Include(u => u.Rol)
//            .FirstOrDefaultAsync(u =>
//            (!string.IsNullOrEmpty(request.Email) && u.Email == request.Email) ||
//        (!string.IsNullOrEmpty(request.Usuario) && u.UsuarioNombre == request.Usuario),

//        cancellationToken);

//        if (usuario == null || !usuario.Activo)
//        {
//            return Result.Unauthorized("Credenciales inválidas");
//        }

//        var password = Functions.Decrypt(usuario.Contrasena);

//        if (request.Contrasenia != password)
//        {
//            return Result.Unauthorized("Credenciales inválidas");
//        }
//        // bool contrasenaValida = BCrypt.Net.BCrypt.Verify(
//        //    request.Contrasenia,
//        //    usuario.Contrasena
//        //);
//        // if (!contrasenaValida)
//        // {
//        //     return Result.Unauthorized("Credenciales inválidas");
//        // }

//        var loginResponse = new UsuarioLoginDto
//        {
//            Id = usuario.Id,
//            NombreCompleto = usuario.NombreCompleto,
//            Email = usuario.Email,
//            UsuarioNombre = usuario.UsuarioNombre,
//            Role = usuario.Rol.sRol,
//            RefreshToken = _jwtService.GenerateRefreshToken(),
//            TokenExpiration = DateTime.UtcNow.AddMinutes(60)
//        };

//        // Generar JWT token usando el servicio real
//        loginResponse.Token = _jwtService.GenerateToken(loginResponse);

//        return Result.Success(loginResponse, "Login exitoso");
//    }
//}

//public class LoginCommandDummyHandler : ICommandHandler<LoginCommand, Result<UsuarioLoginDto>>
//{
//    private readonly IJwtService _jwtService;

//    public LoginCommandDummyHandler(IJwtService jwtService)
//    {
//        _jwtService = jwtService;
//    }

//    public async Task<Result<UsuarioLoginDto>> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
//    {
//        // Credenciales hardcodeadas
//        const string emailValido = "lalo.ariasr@gmail.com";
//        const string usuarioValido = "lalo.ariasr";
//        const string passwordValida = "default";

//        // Validar credenciales
//        bool credencialesValidas =
//            (request.Email == emailValido || request.Usuario == usuarioValido) &&
//            request.Contrasenia == passwordValida;

//        if (!credencialesValidas)
//        {
//            return Result.Unauthorized("Credenciales inválidas");
//        }

//        // Datos fijos del usuario dummy
//        var loginResponse = new UsuarioLoginDto
//        {
//            Id = 1,
//            NombreCompleto = "Eduardo Arias",
//            Email = emailValido,
//            UsuarioNombre = usuarioValido,
//            Role = "Admin",
//            RefreshToken = _jwtService.GenerateRefreshToken(),
//            TokenExpiration = DateTime.UtcNow.AddMinutes(60)
//        };

//        // Generar JWT token
//        loginResponse.Token = _jwtService.GenerateToken(loginResponse);

//        return Result.Success(loginResponse, "Login exitoso");
//    }
//}

public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<UsuarioLoginDto>>
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<Result<UsuarioLoginDto>> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
    {
        // 1. Buscar el usuario por Email o Username
        var usuario = await _userManager.FindByEmailAsync(request.Email ?? "")
                      ?? await _userManager.FindByNameAsync(request.Usuario ?? "");

        if (usuario == null)
        {
            return Result.Unauthorized("Credenciales inválidas");
        }

        // 2. Verificar la contraseña usando SignInManager
        // El tercer parámetro 'false' es para lockoutOnFailure (bloqueo por intentos fallidos)
        var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Contrasenia, false);

        if (!resultado.Succeeded)
        {
            return Result.Unauthorized("Credenciales inválidas");
        }

        // 3. Obtener roles (opcional, pero recomendado para el DTO)
        var roles = await _userManager.GetRolesAsync(usuario);
        var rolPrincipal = roles.FirstOrDefault() ?? "Usuario";

        // 4. Mapear al DTO
        var loginResponse = new UsuarioLoginDto
        {
            Id = usuario.Id,
            NombreCompleto = usuario.NombreCompleto, // Asumiendo que esta propiedad existe en SYS_Usuario
            Email = usuario.Email,
            UsuarioNombre = usuario.UserName,
            Role = rolPrincipal,
            RefreshToken = _jwtService.GenerateRefreshToken(),
            TokenExpiration = DateTime.UtcNow.AddMinutes(60)
        };

        // 5. Generar JWT
        loginResponse.Token = _jwtService.GenerateToken(loginResponse);

        return Result.Success(loginResponse, "Login exitoso");
    }
}