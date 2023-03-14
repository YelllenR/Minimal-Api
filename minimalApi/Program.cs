//Construction of postgres sql connexion string to use in the DbContext

using AutoMapper;
using minimalApi.Data;
using minimalApi.Dtos;
using minimalApi.Models;

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

app.MapPost(
    "api/v1/commands",
    async (ICommandRepo repo, IMapper mapper, CommandCreateDto cmdCreateDto) =>
    {
        //Create a model to inject data in DB - Creating a model from the dto
        var commandModel = mapper.Map<Command>(cmdCreateDto);

        //Creates a new data line
        await repo.CreateCommand(commandModel);

        //Saves changes after creation
        await repo.SaveChangesAsync();

        //Get the generated id
        var cmdReadDto = mapper.Map<CommandReadDto>(commandModel);

        return Results.Created($"api/v1/commands/{cmdReadDto.Id}", cmdCreateDto);
    }
);

app.MapPut(
    "api/v1/commands/{id}",
    async (ICommandRepo repo, IMapper mapper, int id, CommandUpdateDto cmdUpdateDto) =>
    {
        var command = await repo.GetCommandById(id);

        if (command is null)
        {
            return Results.NotFound();
        }

        mapper.Map(cmdUpdateDto, command);

        await repo.SaveChangesAsync();

        return Results.NoContent();
    }
);

app.MapDelete(
    "api/v1/commands/{id}",
    async (ICommandRepo repo, IMapper mapper, int id) =>
    {
        var command = await repo.GetCommandById(id);

        if (command is null)
        {
            return Results.NotFound();
        }
        repo.DeleteCommand(command);
        await repo.SaveChangesAsync();

        return Results.NoContent();
    }
);

app.Run();
