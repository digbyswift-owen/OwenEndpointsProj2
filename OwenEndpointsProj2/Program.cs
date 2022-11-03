using MediatR;
using NHibernate;
using OwenEndpointsProj2.Data;


ISessionFactory sessionFactory = NHibernateHelper.CreateSessionFactory();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddSingleton<NHibernate.ISessionFactory>(sessionFactory);
builder.Services.AddScoped<NHibernate.ISession>(factory => sessionFactory.OpenSession());


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
