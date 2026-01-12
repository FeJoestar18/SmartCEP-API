using Microsoft.EntityFrameworkCore;
using SmartCep.Domain.Entities;
using SmartCep.Domain.Interfaces;
using SmartCep.Infrastructure.Persistence;
using SmartCep.Infrastructure.Persistence.Entities;

namespace SmartCep.Infrastructure.Repositories;

public class CodeRepository(AppDbContext context) : ICodeRepository
{
    public async Task<Code?> GetByPostalCodeAsync(string postalCode)
    {
        var entity = await context.PostalCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PostalCode == postalCode);

        if (entity == null)
            return null;

        return new Code(
            entity.PostalCode,
            entity.Street,
            entity.Neighborhood,
            entity.City,
            entity.State
        );
    }

    public async Task SaveAsync(Code code)
    {
        var entity = new CodeEntity
        {
            PostalCode = code.PostalCode,
            Street = code.Street,
            Neighborhood = code.Neighborhood,
            City = code.City,
            State = code.State
        };

        context.PostalCodes.Add(entity);
        await context.SaveChangesAsync();
    }
}

