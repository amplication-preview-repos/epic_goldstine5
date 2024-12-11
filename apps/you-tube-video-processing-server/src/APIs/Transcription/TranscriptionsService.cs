using YouTubeVideoProcessing.Infrastructure;

namespace YouTubeVideoProcessing.APIs;

public class TranscriptionsService : TranscriptionsServiceBase
{
    public TranscriptionsService(YouTubeVideoProcessingDbContext context)
        : base(context) { }
}
