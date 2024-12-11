using YouTubeVideoProcessing.Infrastructure;

namespace YouTubeVideoProcessing.APIs;

public class VideosService : VideosServiceBase
{
    public VideosService(YouTubeVideoProcessingDbContext context)
        : base(context) { }
}
