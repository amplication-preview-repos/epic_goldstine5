using Microsoft.AspNetCore.Mvc;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;

namespace YouTubeVideoProcessing.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TranslationsControllerBase : ControllerBase
{
    protected readonly ITranslationsService _service;

    public TranslationsControllerBase(ITranslationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Translation
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Translation>> CreateTranslation(TranslationCreateInput input)
    {
        var translation = await _service.CreateTranslation(input);

        return CreatedAtAction(nameof(Translation), new { id = translation.Id }, translation);
    }

    /// <summary>
    /// Delete one Translation
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTranslation(
        [FromRoute()] TranslationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteTranslation(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Translations
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Translation>>> Translations(
        [FromQuery()] TranslationFindManyArgs filter
    )
    {
        return Ok(await _service.Translations(filter));
    }

    /// <summary>
    /// Meta data about Translation records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TranslationsMeta(
        [FromQuery()] TranslationFindManyArgs filter
    )
    {
        return Ok(await _service.TranslationsMeta(filter));
    }

    /// <summary>
    /// Get one Translation
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Translation>> Translation(
        [FromRoute()] TranslationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Translation(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Translation
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateTranslation(
        [FromRoute()] TranslationWhereUniqueInput uniqueId,
        [FromQuery()] TranslationUpdateInput translationUpdateDto
    )
    {
        try
        {
            await _service.UpdateTranslation(uniqueId, translationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
