using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.APIs.Dtos;

namespace YouTubeVideoProcessing.APIs;

public interface ITranscriptionsService
{
    /// <summary>
    /// Create one Transcription
    /// </summary>
    public Task<Transcription> CreateTranscription(TranscriptionCreateInput transcription);

    /// <summary>
    /// Delete one Transcription
    /// </summary>
    public Task DeleteTranscription(TranscriptionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Transcriptions
    /// </summary>
    public Task<List<Transcription>> Transcriptions(TranscriptionFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Transcription records
    /// </summary>
    public Task<MetadataDto> TranscriptionsMeta(TranscriptionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Transcription
    /// </summary>
    public Task<Transcription> Transcription(TranscriptionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Transcription
    /// </summary>
    public Task UpdateTranscription(
        TranscriptionWhereUniqueInput uniqueId,
        TranscriptionUpdateInput updateDto
    );
}
