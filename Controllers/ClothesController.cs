using Api.Repos;
using Api.DTO;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

//Controller for handling clothes - adding removing and etc
[ApiController]
[Route("/[controller]")]
public class ClothesController : ControllerBase
{
    //Get clothes by category
    [HttpGet]
    public async Task<IActionResult> GetClothesByCategory(ClothesRepo clothesRepo, [FromQuery] ClothesCategory? category)
    {
        var clothes = await clothesRepo.GetPieces();
        if (category is null) return Ok(clothes);
        var data = clothes.Where(p => p.Category == category);
        return Ok(data);
    }
    
    //Get a particular piece by ID
    [HttpGet]
    [Route("/{id:guid}")]
    public async Task<IActionResult> GetPiece(ClothesRepo clothesRepo, [FromRoute] Guid id)
    {
        var piece = await clothesRepo.GetPiece(id);
        if (piece is null)
        {
            return NotFound("No such piece"); 
        }
        return Ok(piece);
    }
    
    //Add a piece
    [HttpPost]
    public async Task<IActionResult> AddPiece(ClothesRepo clothesRepo, [FromForm] PieceAddReq req)
    {
        var extension = Path.GetExtension(req.Image.FileName);
        var allowedExtensions = new[] { ".png", ".jpeg", ".jpg" };
        if (!allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            return BadRequest("Invalid image format");

        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = $"/app/uploads/images/{fileName}"; 

        await using var stream = System.IO.File.Create(filePath);
        await req.Image.CopyToAsync(stream);
        
        ClothesPiece piece = new(Guid.NewGuid(), req.Name, req.Desc, req.Category, $"/images/{fileName}");
        await clothesRepo.AddPiece(piece);
        return Ok();
    }

    //Delete a piece by ID
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteClothes(ClothesRepo clothesRepo, [FromRoute] Guid id)
    {
        var res = await clothesRepo.DeletePiece(id);
        return res ? NoContent() : NotFound("No such piece");
    }
}