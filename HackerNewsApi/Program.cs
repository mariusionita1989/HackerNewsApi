using HackerNewsApi.Config;
using HackerNewsApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // add memory cache
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.Configure<List<ApiConfiguration>>(builder.Configuration.GetSection("ExternalApi"));
builder.Services.AddHttpClient<IHackerNewsService,HackerNewsService>();
builder.Services.AddScoped<IHackerNewsService,HackerNewsService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
