using Microsoft.AspNetCore.Mvc;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;

namespace YouTubeVideoProcessing.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class LanguagesControllerBase : ControllerBase
{
    protected readonly ILanguagesService _service;

    public LanguagesControllerBase(ILanguagesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Language
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Language>> CreateLanguage(LanguageCreateInput input)
    {
        var language = await _service.CreateLanguage(input);

        return CreatedAtAction(nameof(Language), new { id = language.Id }, language);
    }

    /// <summary>
    /// Delete one Language
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteLanguage([FromRoute()] LanguageWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteLanguage(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Languages
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Language>>> Languages(
        [FromQuery()] LanguageFindManyArgs filter
    )
    {
        return Ok(await _service.Languages(filter));
    }

    /// <summary>
    /// Meta data about Language records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> LanguagesMeta(
        [FromQuery()] LanguageFindManyArgs filter
    )
    {
        return Ok(await _service.LanguagesMeta(filter));
    }

    /// <summary>
    /// Get one Language
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Language>> Language(
        [FromRoute()] LanguageWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Language(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Language
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateLanguage(
        [FromRoute()] LanguageWhereUniqueInput uniqueId,
        [FromQuery()] LanguageUpdateInput languageUpdateDto
    )
    {
        try
        {
            await _service.UpdateLanguage(uniqueId, languageUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
