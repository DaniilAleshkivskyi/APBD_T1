using System.ComponentModel.DataAnnotations;

namespace APBD_TEST_TEMPLATE.DTOs;

public class ProductTypeDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string name { get; set; }
}