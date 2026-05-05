using APBD_TEST_TEMPLATE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APBD_TEST_TEMPLATE.Services;

public interface IMakerService
{
    public Task<List<MakerDTO>> getAllMakersAsync(string? name);
    public Task CreateMakerAsync(MakerDTO request);
}