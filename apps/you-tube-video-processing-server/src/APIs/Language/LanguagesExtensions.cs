using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs.Extensions;

public static class LanguagesExtensions
{
    public static Language ToDto(this LanguageDbModel model)
    {
        return new Language
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static LanguageDbModel ToModel(
        this LanguageUpdateInput updateDto,
        LanguageWhereUniqueInput uniqueId
    )
    {
        var language = new LanguageDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            language.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            language.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return language;
    }
}
