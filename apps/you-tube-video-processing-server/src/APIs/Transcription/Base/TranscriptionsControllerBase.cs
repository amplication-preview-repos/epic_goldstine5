using Microsoft.AspNetCore.Mvc;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;

namespace YouTubeVideoProcessing.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TranscriptionsControllerBase : ControllerBase
{
    protected readonly ITranscriptionsService _service;

    public TranscriptionsControllerBase(ITranscriptionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Transcription
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Transcription>> CreateTranscription(
        TranscriptionCreateInput input
    )
    {
        var transcription = await _service.CreateTranscription(input);

        return CreatedAtAction(nameof(Transcription), new { id = transcription.Id }, transcription);
    }

    /// <summary>
    /// Delete one Transcription
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTranscription(
        [FromRoute()] TranscriptionWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteTranscription(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Transcriptions
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Transcription>>> Transcriptions(
        [FromQuery()] TranscriptionFindManyArgs filter
    )
    {
        return Ok(await _service.Transcriptions(filter));
    }

    /// <summary>
    /// Meta data about Transcription records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TranscriptionsMeta(
        [FromQuery()] TranscriptionFindManyArgs filter
    )
    {
        return Ok(await _service.TranscriptionsMeta(filter));
    }

    /// <summary>
    /// Get one Transcription
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Transcription>> Transcription(
        [FromRoute()] TranscriptionWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Transcription(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Transcription
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateTranscription(
        [FromRoute()] TranscriptionWhereUniqueInput uniqueId,
        [FromQuery()] TranscriptionUpdateInput transcriptionUpdateDto
    )
    {
        try
        {
            await _service.UpdateTranscription(uniqueId, transcriptionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
