using Microsoft.AspNetCore.Mvc;

namespace YouTubeVideoProcessing.APIs;

[ApiController()]
public class VideosController : VideosControllerBase
{
    public VideosController(IVideosService service)
        : base(service) { }
}
