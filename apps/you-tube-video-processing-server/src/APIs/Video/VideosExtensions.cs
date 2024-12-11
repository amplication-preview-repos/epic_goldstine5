using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs.Extensions;

public static class VideosExtensions
{
    public static Video ToDto(this VideoDbModel model)
    {
        return new Video
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static VideoDbModel ToModel(
        this VideoUpdateInput updateDto,
        VideoWhereUniqueInput uniqueId
    )
    {
        var video = new VideoDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            video.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            video.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return video;
    }
}
