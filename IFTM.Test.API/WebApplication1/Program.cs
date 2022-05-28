using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var connectionString = builder.Configuration.GetConnectionString("Test");
builder.Services.AddDbContext<TestContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();


app.Run();
