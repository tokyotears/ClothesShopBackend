using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repos;

public class ClothesRepo (AppDbContext db)
{
    //Get piece by name
    public async Task<ClothesPiece?> GetPiece(string name) => await db.Clothes.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
    
    //Get piece by ID
    public async Task<ClothesPiece?> GetPiece(Guid id) => await db.Clothes.FindAsync(id);
    
    //Get all clothes
    public async Task<List<ClothesPiece>> GetPieces() => await db.Clothes.AsNoTracking().ToListAsync();
    
    //Add a piece
    public async Task AddPiece(ClothesPiece piece)
    {
        db.Clothes.Add(piece);
        await db.SaveChangesAsync();
    }
    
    //Delete a piece
    public async Task<bool> DeletePiece(Guid id)
    {
        var piece = await GetPiece(id);
        if (piece is null) return false;
        db.Clothes.Remove(piece);
        await db.SaveChangesAsync();
        return true;
    }
}