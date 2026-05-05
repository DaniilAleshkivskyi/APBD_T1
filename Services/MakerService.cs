using APBD_TEST_TEMPLATE.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace APBD_TEST_TEMPLATE.Services;

public class MakerService : IMakerService
{
    private readonly string _connectionString;

    public MakerService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")
                            ?? throw new InvalidOperationException("Missing 'Default' connection string.");
    }
    public async Task<List<MakerDTO>> getAllMakersAsync(string? name)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var makersById = new Dictionary<int, MakerDTO>();
        await using (var rentalsCommand = new SqlCommand(@"
        SELECT m.id,m.name,
               p.id,p.name,p.description,p.StickerPrice,
               pt.id,pt.name,
               v.code,v.name,vp.Amount,vp.PricePerUnit 
        from Makers m
        JOIN Products p on m.id = p.MakerId
        JOIN ProductTypes pt on p.ProductTypeId = pt.Id
        JOIN VendorProducts vp on p.id = vp.ProductId
        JOIN Vendors v on vp.VendorCode = V.Code
        WHERE m.Name like @makerName;", connection))
        {
            if (name != null)
            {
                rentalsCommand.Parameters.AddWithValue("@makerName", name.Trim() + "%");
            }
            else
            {
                rentalsCommand.Parameters.AddWithValue("@makerName", "%");
            }
            
            await using var reader = await rentalsCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                
                var makerId = reader.GetInt32(0);

                if (!makersById.TryGetValue(makerId, out var maker))
                {
                    maker = new MakerDTO
                    {
                        Id = makerId,
                        Name = reader.GetString(1),
                        Products = new List<ProductDTO>()
                    };
                    makersById.Add(makerId, maker);
                }
                if (!reader.IsDBNull(2))
                {
                    maker.Products.Add(new ProductDTO()
                    {
                        Id = reader.GetInt32(2),
                        Name = reader.GetString(3),
                        Description =  reader.GetString(4),
                        strickerPrice =  reader.GetDecimal(5),
                        ProductType = new ProductTypeDTO()
                        {
                            Id = reader.GetInt32(6),
                            name = reader.GetString(7)
                        },
                        vendors = new List<VendorsDTO>()
                    });
                }
            }
        }
        return makersById.Values.ToList();
    }

    public async Task CreateMakerAsync(MakerDTO request)
    {
        
        /*
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();
        
        try
        {
            
            for (var i = 0; i < request.Products.Count; i++)
            {
                await using var prod = new SqlCommand(
                    @"INSERT INTO Products (Name, Description, StickerPrice, ProductTypeId, MakerId)
                      VALUES (@name, @description, @stickerPrice,@productTypeId,@makerId);",
                    connection,
                    transaction);
                
                prod.Parameters.AddWithValue("@name", request.Products[i].Name );
                prod.Parameters.AddWithValue("@description", request.Products[i].Description);
                prod.Parameters.AddWithValue(@sti)

                await itemCommand.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }*/
    }
}