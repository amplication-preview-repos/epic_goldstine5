using YouTubeVideoProcessing.APIs;

namespace YouTubeVideoProcessing;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ILanguagesService, LanguagesService>();
        services.AddScoped<ITranscriptionsService, TranscriptionsService>();
        services.AddScoped<ITranslationsService, TranslationsService>();
        services.AddScoped<IVideosService, VideosService>();
    }
}
