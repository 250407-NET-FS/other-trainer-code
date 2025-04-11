//Up here, like any .cs file, we can throw in our using statements for packages or namespaces
// that we may need
using System.Text.Json;
using Library.Models;
using Library.Repositories;
using Library.Services;

// Here is our builder
var builder = WebApplication.CreateBuilder(args);

//==== Dependency Injection Area ====
builder.Services.AddSingleton<IMemberRepository, JsonMemberRepository>();
builder.Services.AddSingleton<IBookRepository, JsonBookRepository>();

//Adding swagger to my dependencies
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "LibraryAPI";
    config.Title = "LibraryAPI";
    config.Version = "v1";
});

// Here the builder takes all of our DI and middleware stuff and creates our app.
var app = builder.Build();

//Telling the app to use swagger, pulling it from the DI container in ASP.NET
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "LibraryAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

//Book endpoints
app.MapGet(
    "/books",
    (IBookRepository repo) =>
    {
        try
        {
            return Results.Ok(repo.GetAllBooks());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

app.MapPost(
    "/books",
    (Book book, IBookRepository repo) =>
    {
        try
        {
            var createdBook = repo.AddBook(book);
            return Results.Created($"/books/{createdBook.Isbn}", createdBook);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 400);
        }
    }
);

//Member endpoints
app.MapGet(
    "/members",
    (IMemberRepository repo) =>
    {
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
    }
);

app.MapPost(
    "/members",
    (Member memberToAdd, IMemberRepository repo) =>
    {
        try
        {
            return Results.Ok(repo.AddMember(memberToAdd));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 400); //We can return specific codes with Results.Problem if we want to
        }
    }
);

//Checkout endpoints

app.MapPost(
    "/checkouts",
    (CheckoutRequestDTO checkoutRequest) =>
    {
        try
        {
            //Some service layer method call goes here
            Checkout checkout = CheckoutService.CheckoutBook(checkoutRequest);
            return Results.Created(checkout);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

app.MapPut(
    "/checkouts/return/{isbn}",
    (string isbn) =>
    {
        try
        {
            //Here we will call our service layer method
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

// Finally, this is what runs our app.
app.Run();
