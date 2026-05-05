using System.ComponentModel.DataAnnotations;

namespace APBD_TEST_TEMPLATE.DTOs;

public class ProductDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal strickerPrice { get; set; }
    [Required]
    public ProductTypeDTO ProductType { get; set; }
    [Required] 
    public List<VendorsDTO> vendors { get; set; } = new();
}