using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using GraphQLNetExample.Note;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISchema, NotesSchema>(
    services => new NotesSchema(new SelfActivatingServiceProvider(services)));

builder.Services.AddGraphQL(
    options =>
    {
        options.EnableMetrics = true;
    }).AddSystemTextJson();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GraphQLExample", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQLNetExample v1"));
    app.UseGraphQLAltair();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseGraphQL<ISchema>();

app.Run();
