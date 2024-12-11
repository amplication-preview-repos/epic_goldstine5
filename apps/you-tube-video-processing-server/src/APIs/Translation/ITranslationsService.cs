using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;

namespace YouTubeVideoProcessing.APIs;

public interface ITranslationsService
{
    /// <summary>
    /// Create one Translation
    /// </summary>
    public Task<Translation> CreateTranslation(TranslationCreateInput translation);

    /// <summary>
    /// Delete one Translation
    /// </summary>
    public Task DeleteTranslation(TranslationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Translations
    /// </summary>
    public Task<List<Translation>> Translations(TranslationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Translation records
    /// </summary>
    public Task<MetadataDto> TranslationsMeta(TranslationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Translation
    /// </summary>
    public Task<Translation> Translation(TranslationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Translation
    /// </summary>
    public Task UpdateTranslation(
        TranslationWhereUniqueInput uniqueId,
        TranslationUpdateInput updateDto
    );
}
