//Up here, like any .cs file, we can throw in our using statements for packages or namespaces
// that we may need
using System.Text.Json;
using Library.Models;
using Library.Repositories;

// Here is our builder
var builder = WebApplication.CreateBuilder(args);

//==== Dependency Injection Area ====
builder.Services.AddSingleton<IMemberRepository, JsonMemberRepository>();

// Here the builder takes all of our DI and middleware stuff and creates our app.
var app = builder.Build();

//Book endpoints


//Member endpoints
app.MapGet("/members", (IMemberRepository repo) => {
    try
    {
        return Results.Ok(repo.GetAllMembers());
    }
    catch (Exception ex)
    {
        // Using the Results prebuilt object to return the appropriate code
        // if anything goes wrong
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/members", (Member memberToAdd, IMemberRepository repo) => {

    try
    {
        return Results.Ok(repo.AddMember(memberToAdd));
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message, statusCode: 400); //We can return specific codes with Results.Problem if we want to
    }

});


//Checkout endpoints


// Finally, this is what runs our app.
app.Run();
