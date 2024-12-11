using Microsoft.EntityFrameworkCore;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;
using YouTubeVideoProcessing.APIs.Extensions;
using YouTubeVideoProcessing.Infrastructure;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs;

public abstract class TranslationsServiceBase : ITranslationsService
{
    protected readonly YouTubeVideoProcessingDbContext _context;

    public TranslationsServiceBase(YouTubeVideoProcessingDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Translation
    /// </summary>
    public async Task<Translation> CreateTranslation(TranslationCreateInput createDto)
    {
        var translation = new TranslationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            translation.Id = createDto.Id;
        }

        _context.Translations.Add(translation);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TranslationDbModel>(translation.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Translation
    /// </summary>
    public async Task DeleteTranslation(TranslationWhereUniqueInput uniqueId)
    {
        var translation = await _context.Translations.FindAsync(uniqueId.Id);
        if (translation == null)
        {
            throw new NotFoundException();
        }

        _context.Translations.Remove(translation);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Translations
    /// </summary>
    public async Task<List<Translation>> Translations(TranslationFindManyArgs findManyArgs)
    {
        var translations = await _context
            .Translations.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return translations.ConvertAll(translation => translation.ToDto());
    }

    /// <summary>
    /// Meta data about Translation records
    /// </summary>
    public async Task<MetadataDto> TranslationsMeta(TranslationFindManyArgs findManyArgs)
    {
        var count = await _context.Translations.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Translation
    /// </summary>
    public async Task<Translation> Translation(TranslationWhereUniqueInput uniqueId)
    {
        var translations = await this.Translations(
            new TranslationFindManyArgs { Where = new TranslationWhereInput { Id = uniqueId.Id } }
        );
        var translation = translations.FirstOrDefault();
        if (translation == null)
        {
            throw new NotFoundException();
        }

        return translation;
    }

    /// <summary>
    /// Update one Translation
    /// </summary>
    public async Task UpdateTranslation(
        TranslationWhereUniqueInput uniqueId,
        TranslationUpdateInput updateDto
    )
    {
        var translation = updateDto.ToModel(uniqueId);

        _context.Entry(translation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Translations.Any(e => e.Id == translation.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
