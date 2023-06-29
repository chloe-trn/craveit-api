using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CraveIt.API.Data;
using CraveIt.API.Repositories.QuizRepository;
using CraveIt.API.Repositories.ResultRepository;
using CraveIt.API.Services.ProcessQuizService;
using CraveIt.API.Services.RandomizerService;
using CraveIt.API.Services.UserService;
using CraveIt.API.Services.YelpService;
using CraveIt.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container:

// Adding Db connection
builder.Services.AddDbContext<CraveItApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Adding Identity service
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CraveItApiDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication and Jwt service
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("JWTAppSettings:ValidAudience").Value,
        ValidIssuer = builder.Configuration.GetSection("JWTAppSettings:ValidIssuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTAppSettings:Secret").Value))
    };
});
builder.Services.Configure<JWTAppSettings>(builder.Configuration.GetSection("JWTAppSettings"));

// Configure other services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IYelpService, YelpService>();
builder.Services.AddHttpClient<IYelpService, YelpService>();
builder.Services.AddScoped<IRandomizerService, RandomizerService>();
builder.Services.AddScoped<IProcessQuizService, ProcessQuizService>();

// Configure repositories
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();

// Add controllers and API explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add cors
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader())
);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Enable authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();