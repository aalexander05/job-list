using JobListAPI;
using JobListAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
    {
        Description = "Enter a valid token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        BearerFormat = "Bearer",
        Scheme = "Bearer",
        OpenIdConnectUrl = new Uri("https://login.microsoftonline.com/f5b9491d-79c7-44ef-8f04-d4062c7a780a/v2.0/.well-known/openid-configuration"),
        Flows = new OpenApiOAuthFlows()
        {
            AuthorizationCode = new OpenApiOAuthFlow()
            {
                TokenUrl = new Uri("https://login.microsoftonline.com/f5b9491d-79c7-44ef-8f04-d4062c7a780a/oauth2/v2.0/token"),
                AuthorizationUrl = new Uri("https://login.microsoftonline.com/f5b9491d-79c7-44ef-8f04-d4062c7a780a/oauth2/v2.0/authorize"),
                Scopes = new Dictionary<string, string>() {
                    //{ $"{builder.Configuration["AzureAd:ClientId"]}/.default", "scopes-default-test" }
                    { $"{builder.Configuration["AzureAd:ClientId"]}/myapi.read", "TEST TEST TEST" }
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Token",
                                        Type = ReferenceType.SecurityScheme
                                    },
                                    Scheme = "oauth2",
                                    Name = "oauth2",
                                    In = ParameterLocation.Header,
                                },
                                new List<string>()
                            }
                        });
});

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

builder.Services.AddApplicationServices();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000");
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                      });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthUsePkce();
        options.OAuthClientId(builder.Configuration["AzureAd:ClientId"]);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
