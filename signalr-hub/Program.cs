using signalr_lab.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Hub
builder.Services.AddSignalR();

builder.Services.AddCors(options => {
    options
        .AddPolicy("CORSPolicy", builder
            => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddControllers();
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

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<MessageHub>("/case-alerts");

app.Run();
