//Up here, like any .cs file, we can throw in our using statements for packages or namespaces
// that we may need
using System.Text.Json;

// Here is our builder
var builder = WebApplication.CreateBuilder(args);

//==== Dependency Injection Area ====


// Here the builder takes all of our DI and middleware stuff and creates our app.
var app = builder.Build();

//Book endpoints


//Member endpoints


//Checkout endpoints


// Finally, this is what runs our app.
app.Run();
