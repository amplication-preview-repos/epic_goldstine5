using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;

namespace YouTubeVideoProcessing.APIs;

public interface IVideosService
{
    /// <summary>
    /// Create one Video
    /// </summary>
    public Task<Video> CreateVideo(VideoCreateInput video);

    /// <summary>
    /// Delete one Video
    /// </summary>
    public Task DeleteVideo(VideoWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Videos
    /// </summary>
    public Task<List<Video>> Videos(VideoFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Video records
    /// </summary>
    public Task<MetadataDto> VideosMeta(VideoFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Video
    /// </summary>
    public Task<Video> Video(VideoWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Video
    /// </summary>
    public Task UpdateVideo(VideoWhereUniqueInput uniqueId, VideoUpdateInput updateDto);
}
