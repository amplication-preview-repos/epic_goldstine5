using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs.Extensions;

public static class TranscriptionsExtensions
{
    public static Transcription ToDto(this TranscriptionDbModel model)
    {
        return new Transcription
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TranscriptionDbModel ToModel(
        this TranscriptionUpdateInput updateDto,
        TranscriptionWhereUniqueInput uniqueId
    )
    {
        var transcription = new TranscriptionDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            transcription.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            transcription.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return transcription;
    }
}
