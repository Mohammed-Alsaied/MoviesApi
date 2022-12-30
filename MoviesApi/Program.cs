using Microsoft.OpenApi.Models;
using MoviesApi.DbContexts;
using MoviesApi.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

//Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(dbContextOptions =>
dbContextOptions.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();

builder.Services.AddAutoMapper(typeof(Program));
//CORS => Cross Origin Resource Sharing
//Disabled By Default

builder.Services.AddCors();//Enable CORS

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", info: new OpenApiInfo
    {
        Version = "v1",
        Title = "MoviesApi",
        Description = "My First",
        TermsOfService = new Uri("https://www.google.com"),
        Contact = new OpenApiContact
        {
            Name = "Mohamed",
            Email = "test@domain.com",
            Url = new Uri("https://www.google.com"),
        },
        License = new OpenApiLicense
        {
            Name = "My License",
            Url = new Uri("https://www.google.com"),
        }
    });

    options.AddSecurityDefinition("MoviesApi", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT Key",

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Name="Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
        }
    });
});


var app = builder.Build();

//MidllWare
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Best Place to Write CORS Here
/*app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().WithOrigins();*///Write website you want to Access the Api High Security
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
