using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs.Extensions;

public static class TranslationsExtensions
{
    public static Translation ToDto(this TranslationDbModel model)
    {
        return new Translation
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TranslationDbModel ToModel(
        this TranslationUpdateInput updateDto,
        TranslationWhereUniqueInput uniqueId
    )
    {
        var translation = new TranslationDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            translation.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            translation.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return translation;
    }
}
