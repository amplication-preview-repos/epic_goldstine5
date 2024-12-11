using Microsoft.EntityFrameworkCore;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;
using YouTubeVideoProcessing.APIs.Extensions;
using YouTubeVideoProcessing.Infrastructure;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs;

public abstract class VideosServiceBase : IVideosService
{
    protected readonly YouTubeVideoProcessingDbContext _context;

    public VideosServiceBase(YouTubeVideoProcessingDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Video
    /// </summary>
    public async Task<Video> CreateVideo(VideoCreateInput createDto)
    {
        var video = new VideoDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            video.Id = createDto.Id;
        }

        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<VideoDbModel>(video.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Video
    /// </summary>
    public async Task DeleteVideo(VideoWhereUniqueInput uniqueId)
    {
        var video = await _context.Videos.FindAsync(uniqueId.Id);
        if (video == null)
        {
            throw new NotFoundException();
        }

        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Videos
    /// </summary>
    public async Task<List<Video>> Videos(VideoFindManyArgs findManyArgs)
    {
        var videos = await _context
            .Videos.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return videos.ConvertAll(video => video.ToDto());
    }

    /// <summary>
    /// Meta data about Video records
    /// </summary>
    public async Task<MetadataDto> VideosMeta(VideoFindManyArgs findManyArgs)
    {
        var count = await _context.Videos.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Video
    /// </summary>
    public async Task<Video> Video(VideoWhereUniqueInput uniqueId)
    {
        var videos = await this.Videos(
            new VideoFindManyArgs { Where = new VideoWhereInput { Id = uniqueId.Id } }
        );
        var video = videos.FirstOrDefault();
        if (video == null)
        {
            throw new NotFoundException();
        }

        return video;
    }

    /// <summary>
    /// Update one Video
    /// </summary>
    public async Task UpdateVideo(VideoWhereUniqueInput uniqueId, VideoUpdateInput updateDto)
    {
        var video = updateDto.ToModel(uniqueId);

        _context.Entry(video).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Videos.Any(e => e.Id == video.Id))
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
