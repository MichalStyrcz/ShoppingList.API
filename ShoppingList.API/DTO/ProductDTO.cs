using ShoppingList.API.Model;

namespace ShoppingList.API.DTO;

public class ProductDto
{
    #region Constructor

    public ProductDto()
    {
        Description = string.Empty;
        Handled = false;
        Name = string.Empty;
        Shop = string.Empty;
    }

    #endregion

    #region Methods

    public static ProductDto FromProduct(Product product)
    {
        return new() {
            Description = product.Description,
            Handled = product.Handled,
            Name = product.Name,
            Shop = product.Shop
        };
    }

    public Product ToProduct(int id)
    {
        return new() {
            Description = Description,
            Handled = Handled,
            Id = id,
            Name = Name,
            Shop = Shop
        };
    }

    #endregion

    #region Properties

    public string Description { get; set; }
    public bool Handled { get; set; }
    public string Name { get; set; }
    public string Shop { get; set; }

    #endregion
}
