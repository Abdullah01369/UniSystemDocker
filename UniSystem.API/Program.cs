using GroqSharp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using SharedLayer.Configuration;
using SharedLayer.Options;
using SharedLayer.Services;
using UniSystem.API.Exceptions;
using UniSystem.API.SignalRService;
using UniSystem.Core.Configuration;
using UniSystem.Core.Models;
using UniSystem.Core.Repositories;
using UniSystem.Core.Services;
using UniSystem.Core.UnitOfWork;
using UniSystem.Data.ConnectionDB;
using UniSystem.Data.Repositories;
using UniSystem.Service;
using UniSystem.Service.Services;
using UniSystem.Service.Services.RabbitMQServices;
using UniSystem.Service.Services.ResearcherServices;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(DtoMapper));


#region  rabbitmq
builder.Services.AddSingleton(sp => new ConnectionFactory()
{
    Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMq")),
    DispatchConsumersAsync = true

});
#endregion
#region services
builder.Services.AddSignalR();
builder.Services.Configure<IyzicoOptions>(builder.Configuration.GetSection("IyzicoOptions"));
builder.Services.AddSingleton<RabbitMqClientServices>();
builder.Services.AddSingleton<RabbitMqGradutedService>();
builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.AddSingleton<RabbitMqMailSenderPublisher>();
builder.Services.AddSingleton<SharedLayer.RabbitMQ.RabbitMqPublisherService>();
builder.Services.AddSingleton<SharedLayer.RabbitMQ.RabbitMqClientService>();
builder.Services.AddSingleton<RabbitMqDocumentClientServices>();
builder.Services.AddSingleton<RabbitMqDocumentPublisherServices>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICourseServices, CourseServices>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IGraduateService, GraduateService>();
builder.Services.AddScoped<IDepartmentService, DepartmentServices>();
builder.Services.AddScoped<IMessagesServices, MessagesServices>();
builder.Services.AddScoped<IUserRecordServices, UserRecordServices>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILessonService, ExamsLessonsService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServiceGeneric<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAcademicianLessonService, AcademicianLessonService>();
builder.Services.AddScoped<IResearcherService, ResearcherService>();
#endregion
#region db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("UniSystem.Data");
    });
});

#endregion 
#region identity

builder.Services.AddIdentity<AppUser, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;
    Opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(24); // Token 24 saat geçerli
});

#endregion
#region FORAI
builder.Services.AddScoped<IAIAnalysisService, AIAnalysisService>();

var apikey = builder.Configuration["APIKEY"];
var apimodel = "llama-3.3-70b-versatile";


builder.Services.AddSingleton<IGroqClient>(sp =>

    new GroqClient(apikey, apimodel)
    .SetTemperature(0.5)
    .SetMaxTokens(1024)
    .SetTopP(1)
    .SetStop("NONE")
    .SetStructuredRetryPolicy(5));
#endregion



builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllWithCredentials", policy =>
    {
        policy.SetIsOriginAllowed(_ => true) // Tüm kaynaklara izin verir.
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));  // option pattern
builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHub<VideoHub>("/VideoHub");

//app.UseCors();
app.UseCors("AllowAllWithCredentials");
app.UseHttpsRedirection();
app.UseCustomExceptionHandlers();
app.UseAuthentication(); //  önce olmalý
app.UseAuthorization();
app.MapControllers();
app.Run();
