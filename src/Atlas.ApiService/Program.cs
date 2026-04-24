using Atlas.ApiService;
using Atlas.ApiService.Handlers;
using Atlas.ApiService.Infrastructure;
using Atlas.Application;
using Atlas.Shared.Common;
using Atlas.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;



// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

builder.AddWebServices();
builder.Services.AddControllers();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails
    (options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["server"] = Environment.MachineName;
    };
});

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? ""))
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse(); // Evita el JSON RFC 7807 por defecto
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new ApiResponseDto<object>
            {
                Success = false,
                Message = "No está autorizado. El token es inválido o ha expirado.",
                StatusCode = StatusCodes.Status401Unauthorized,
                Timestamp = DateTime.UtcNow,
                TraceId = context.HttpContext.TraceIdentifier
            });
        }
    };
});

builder.Services.AddAuthorization();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration
        .GetSection("AllowedOrigins")
        .Get<string[]>()
        ?? Array.Empty<string>();

    options.AddPolicy("YggdrasilCorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();
app.UseExceptionHandler();
app.UseStatusCodePages();


#if (!UseAspire)
app.UseHealthChecks("/health");
#endif


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("YggdrasilCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

// Swagger y Scalar solo en desarrollo — no exponer en producción
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pasivos V1");
    });
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API Services Catalogos");
        options.WithTheme(ScalarTheme.DeepSpace);
        options.WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.AsyncHttp);
        options.HideSearch = true;
        options.ShowSidebar = true;
        options.DarkMode = false;
    });

    app.Map("/", () => Results.Redirect("/scalar"));
}


#if (UseAspire)
app.MapDefaultEndpoints();
#endif

app.MapEndpoints();

app.MapControllers();

app.Run();
