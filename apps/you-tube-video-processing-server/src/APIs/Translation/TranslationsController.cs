using Microsoft.AspNetCore.Mvc;

namespace YouTubeVideoProcessing.APIs;

[ApiController()]
public class TranslationsController : TranslationsControllerBase
{
    public TranslationsController(ITranslationsService service)
        : base(service) { }
}
