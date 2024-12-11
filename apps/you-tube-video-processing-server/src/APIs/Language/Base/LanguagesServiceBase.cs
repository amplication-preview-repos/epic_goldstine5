using Microsoft.EntityFrameworkCore;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;
using YouTubeVideoProcessing.APIs.Extensions;
using YouTubeVideoProcessing.Infrastructure;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs;

public abstract class LanguagesServiceBase : ILanguagesService
{
    protected readonly YouTubeVideoProcessingDbContext _context;

    public LanguagesServiceBase(YouTubeVideoProcessingDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Language
    /// </summary>
    public async Task<Language> CreateLanguage(LanguageCreateInput createDto)
    {
        var language = new LanguageDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            language.Id = createDto.Id;
        }

        _context.Languages.Add(language);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<LanguageDbModel>(language.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Language
    /// </summary>
    public async Task DeleteLanguage(LanguageWhereUniqueInput uniqueId)
    {
        var language = await _context.Languages.FindAsync(uniqueId.Id);
        if (language == null)
        {
            throw new NotFoundException();
        }

        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Languages
    /// </summary>
    public async Task<List<Language>> Languages(LanguageFindManyArgs findManyArgs)
    {
        var languages = await _context
            .Languages.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return languages.ConvertAll(language => language.ToDto());
    }

    /// <summary>
    /// Meta data about Language records
    /// </summary>
    public async Task<MetadataDto> LanguagesMeta(LanguageFindManyArgs findManyArgs)
    {
        var count = await _context.Languages.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Language
    /// </summary>
    public async Task<Language> Language(LanguageWhereUniqueInput uniqueId)
    {
        var languages = await this.Languages(
            new LanguageFindManyArgs { Where = new LanguageWhereInput { Id = uniqueId.Id } }
        );
        var language = languages.FirstOrDefault();
        if (language == null)
        {
            throw new NotFoundException();
        }

        return language;
    }

    /// <summary>
    /// Update one Language
    /// </summary>
    public async Task UpdateLanguage(
        LanguageWhereUniqueInput uniqueId,
        LanguageUpdateInput updateDto
    )
    {
        var language = updateDto.ToModel(uniqueId);

        _context.Entry(language).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Languages.Any(e => e.Id == language.Id))
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
