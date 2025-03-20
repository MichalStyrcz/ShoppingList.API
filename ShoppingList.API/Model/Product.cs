namespace ShoppingList.API.Model;

public class Product
{
    #region Constructor

    public Product()
    {
        Description = string.Empty;
        Handled = false;
        Id = 0;
        Name = string.Empty;
        Shop = string.Empty;
    }

    #endregion

    #region Properties

    public string Description { get; set; }
    public bool Handled { get; set; }
    public int Id { get; set; }
    public string Name { get; set;}
    public string Shop { get; set; }

    #endregion
}
