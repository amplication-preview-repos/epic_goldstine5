using Microsoft.AspNetCore.Mvc;

namespace YouTubeVideoProcessing.APIs;

[ApiController()]
public class LanguagesController : LanguagesControllerBase
{
    public LanguagesController(ILanguagesService service)
        : base(service) { }
}
