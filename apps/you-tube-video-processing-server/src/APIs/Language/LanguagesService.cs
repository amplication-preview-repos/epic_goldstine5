using YouTubeVideoProcessing.Infrastructure;

namespace YouTubeVideoProcessing.APIs;

public class LanguagesService : LanguagesServiceBase
{
    public LanguagesService(YouTubeVideoProcessingDbContext context)
        : base(context) { }
}
