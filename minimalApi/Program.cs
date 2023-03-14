//Construction of postgres sql connexion string to use in the DbContext

using AutoMapper;
using minimalApi.Data;
using minimalApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Using dependancy injection on the request to get the repo
app.MapGet(
    "api/v1/commands",
    async (ICommandRepo repo, IMapper mapper) =>
    {
        //Gets any collection in Db
        var commands = await repo.GetAllCommands();
        return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }
);

//Model binding
//int id can be changed to [FromRoute]int id = it will need import the Microsoft.AspNetCore.Mvc
app.MapGet(
    "api/v1/commands/{id}",
    async (ICommandRepo repo, IMapper mapper, int id) =>
    {
        var command = await repo.GetCommandById(id);
        //Check if commande !null
        if (command is not null)
        {
            return Results.Ok(mapper.Map<CommandReadDto>(command));
        }
        return Results.NotFound();
    }
);

app.Run();
