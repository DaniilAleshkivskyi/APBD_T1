using System.ComponentModel.DataAnnotations;

namespace APBD_TEST_TEMPLATE.DTOs;

public class MakerDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public List<ProductDTO> Products { get; set; }
}