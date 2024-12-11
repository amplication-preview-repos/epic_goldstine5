using Microsoft.AspNetCore.Mvc;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;

namespace YouTubeVideoProcessing.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class VideosControllerBase : ControllerBase
{
    protected readonly IVideosService _service;

    public VideosControllerBase(IVideosService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Video
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Video>> CreateVideo(VideoCreateInput input)
    {
        var video = await _service.CreateVideo(input);

        return CreatedAtAction(nameof(Video), new { id = video.Id }, video);
    }

    /// <summary>
    /// Delete one Video
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteVideo([FromRoute()] VideoWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteVideo(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Videos
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Video>>> Videos([FromQuery()] VideoFindManyArgs filter)
    {
        return Ok(await _service.Videos(filter));
    }

    /// <summary>
    /// Meta data about Video records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> VideosMeta([FromQuery()] VideoFindManyArgs filter)
    {
        return Ok(await _service.VideosMeta(filter));
    }

    /// <summary>
    /// Get one Video
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Video>> Video([FromRoute()] VideoWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Video(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Video
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateVideo(
        [FromRoute()] VideoWhereUniqueInput uniqueId,
        [FromQuery()] VideoUpdateInput videoUpdateDto
    )
    {
        try
        {
            await _service.UpdateVideo(uniqueId, videoUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
