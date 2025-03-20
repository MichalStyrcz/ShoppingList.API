using ShoppingList.API.Service;
using ShoppingList.API.DTO;
using ShoppingList.API.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();

var app = builder.Build();

FileService fileService = new () {
    FilePath = builder.Configuration.GetValue("FilePath", "")
};
bool useFile = fileService.FilePath.Length > 0;

List<Product> products = useFile ? fileService.LoadData() : [];

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapGet("/products", () => {
    return Results.Ok(products);
});

app.MapPost("/products", (ProductDto newItemDto) => {
    int newId = (products.Count > 0) ? products[^1].Id + 1 : 1;
    Product newItem = newItemDto.ToProduct(newId);

    products.Add(newItem);
    if (useFile)
        fileService.SaveData(products);

    return Results.Created(string.Format("/products/{0}", newId), newItem);
});

app.MapDelete("/products/{id}", (int id) => {
    Product? existingItem = products.Find(item => item.Id == id);

    if (existingItem != null)
    {
        products.Remove(existingItem);
        if (useFile)
            fileService.SaveData(products);
    }
    return (existingItem != null) ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/products/{id}", (int id) => {
    Product? existingItem = products.Find(item => item.Id == id);

    return (existingItem != null) ? Results.Ok(existingItem) : Results.NotFound();
});

app.MapPut("/products/{id}", (int id, ProductDto itemDto) => {
    Product? existingItem = products.Find(item => item.Id == id);
    
    if (existingItem != null) {
        existingItem.Description = itemDto.Description;
        existingItem.Handled = itemDto.Handled;
        existingItem.Name = itemDto.Name;
        existingItem.Shop = itemDto.Shop;
        if (useFile)
            fileService.SaveData(products);
    }

    return (existingItem != null) ? Results.NoContent() : Results.NotFound();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
