using Microsoft.AspNetCore.Mvc;

namespace YouTubeVideoProcessing.APIs;

[ApiController()]
public class TranscriptionsController : TranscriptionsControllerBase
{
    public TranscriptionsController(ITranscriptionsService service)
        : base(service) { }
}
