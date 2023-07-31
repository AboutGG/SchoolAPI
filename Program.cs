using Microsoft.EntityFrameworkCore;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;
using SchoolAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<SchoolContext>(options =>
{
    //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    //options.UseSqlServer(connectionString);
    options.UseNpgsql(connectionString);
});

// Linking the interfaces to the repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegistryRepository, RegistryRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();

// AtuoMapper Links
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// To make every route in lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
