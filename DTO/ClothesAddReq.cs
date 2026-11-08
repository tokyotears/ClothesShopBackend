using Api.Models;

namespace Api.DTO;

public record PieceAddReq(string Name, string Desc, ClothesCategory Category, IFormFile Image);