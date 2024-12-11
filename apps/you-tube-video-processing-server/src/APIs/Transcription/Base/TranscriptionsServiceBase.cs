using Microsoft.EntityFrameworkCore;
using YouTubeVideoProcessing.APIs;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;
using YouTubeVideoProcessing.APIs.Errors;
using YouTubeVideoProcessing.APIs.Extensions;
using YouTubeVideoProcessing.Infrastructure;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs;

public abstract class TranscriptionsServiceBase : ITranscriptionsService
{
    protected readonly YouTubeVideoProcessingDbContext _context;

    public TranscriptionsServiceBase(YouTubeVideoProcessingDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Transcription
    /// </summary>
    public async Task<Transcription> CreateTranscription(TranscriptionCreateInput createDto)
    {
        var transcription = new TranscriptionDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            transcription.Id = createDto.Id;
        }

        _context.Transcriptions.Add(transcription);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TranscriptionDbModel>(transcription.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Transcription
    /// </summary>
    public async Task DeleteTranscription(TranscriptionWhereUniqueInput uniqueId)
    {
        var transcription = await _context.Transcriptions.FindAsync(uniqueId.Id);
        if (transcription == null)
        {
            throw new NotFoundException();
        }

        _context.Transcriptions.Remove(transcription);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Transcriptions
    /// </summary>
    public async Task<List<Transcription>> Transcriptions(TranscriptionFindManyArgs findManyArgs)
    {
        var transcriptions = await _context
            .Transcriptions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return transcriptions.ConvertAll(transcription => transcription.ToDto());
    }

    /// <summary>
    /// Meta data about Transcription records
    /// </summary>
    public async Task<MetadataDto> TranscriptionsMeta(TranscriptionFindManyArgs findManyArgs)
    {
        var count = await _context.Transcriptions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Transcription
    /// </summary>
    public async Task<Transcription> Transcription(TranscriptionWhereUniqueInput uniqueId)
    {
        var transcriptions = await this.Transcriptions(
            new TranscriptionFindManyArgs
            {
                Where = new TranscriptionWhereInput { Id = uniqueId.Id }
            }
        );
        var transcription = transcriptions.FirstOrDefault();
        if (transcription == null)
        {
            throw new NotFoundException();
        }

        return transcription;
    }

    /// <summary>
    /// Update one Transcription
    /// </summary>
    public async Task UpdateTranscription(
        TranscriptionWhereUniqueInput uniqueId,
        TranscriptionUpdateInput updateDto
    )
    {
        var transcription = updateDto.ToModel(uniqueId);

        _context.Entry(transcription).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Transcriptions.Any(e => e.Id == transcription.Id))
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
