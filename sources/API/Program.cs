using API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(y => y
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
#endif

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x =>
{
    var connString = builder.Configuration.GetConnectionString("FoxRead");
    var serverVersion = ServerVersion.AutoDetect(connString);
    var migrationsAssembly = typeof(API.Data.Migrations.Init).Assembly;
    x.UseMySql(connString, serverVersion, x => x.MigrationsAssembly(migrationsAssembly.FullName));
});

var app = builder.Build();
#if !DEBUG
await app.MigrateDatabaseAsync<AppDbContext>();
#endif

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/api/swagger/v1/swagger.json", $"APIv1");
        c.RoutePrefix = $"api/swagger";
    });
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/api/test", () => "{ \"text\": \"Hello World\" }");
app.MapGet("/api/books", (AppDbContext data) => data.Books);
app.MapPost("/api/books", async (Book book, AppDbContext data) =>
{
    await data.Books.AddAsync(book);
    await data.SaveChangesAsync();
});
app.MapDelete("/api/books/{id}", async (long id, AppDbContext data) =>
{
    var book = await data
        .Books
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

    if (book == null)
    {
        return;
    }

    data.Books.Remove(book);
    await data.SaveChangesAsync();
});

app.Run();
