using YouTubeVideoProcessing.Infrastructure;

namespace YouTubeVideoProcessing.APIs;

public class TranslationsService : TranslationsServiceBase
{
    public TranslationsService(YouTubeVideoProcessingDbContext context)
        : base(context) { }
}
