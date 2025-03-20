using System.Text.Json;
using ShoppingList.API.Model;

namespace ShoppingList.API.Service;

public class FileService
{
    public string FilePath { get; set; }

    public FileService()
    {
        FilePath = "";
    }

    public List<Product> LoadData()
    {
        if (File.Exists(FilePath))
        {
            StreamReader file = File.OpenText(FilePath);
            string content = file.ReadToEnd();
            file.Close();
            List<Product>? result = JsonSerializer.Deserialize<List<Product>>(content);
            return result ?? [];
        }
        else
            return [];
    }

    public void SaveData(List<Product> products)
    {
        string content = JsonSerializer.Serialize(products);
        StreamWriter file = File.CreateText(FilePath);
        file.Write(content);
        file.Close();
    }
}