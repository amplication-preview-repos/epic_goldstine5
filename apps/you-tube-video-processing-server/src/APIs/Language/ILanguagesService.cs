using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;

namespace YouTubeVideoProcessing.APIs;

public interface ILanguagesService
{
    /// <summary>
    /// Create one Language
    /// </summary>
    public Task<Language> CreateLanguage(LanguageCreateInput language);

    /// <summary>
    /// Delete one Language
    /// </summary>
    public Task DeleteLanguage(LanguageWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Languages
    /// </summary>
    public Task<List<Language>> Languages(LanguageFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Language records
    /// </summary>
    public Task<MetadataDto> LanguagesMeta(LanguageFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Language
    /// </summary>
    public Task<Language> Language(LanguageWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Language
    /// </summary>
    public Task UpdateLanguage(LanguageWhereUniqueInput uniqueId, LanguageUpdateInput updateDto);
}
