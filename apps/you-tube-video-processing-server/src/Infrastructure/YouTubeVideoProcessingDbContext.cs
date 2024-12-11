using Microsoft.EntityFrameworkCore;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.Infrastructure;

public class YouTubeVideoProcessingDbContext : DbContext
{
    public YouTubeVideoProcessingDbContext(
        DbContextOptions<YouTubeVideoProcessingDbContext> options
    )
        : base(options) { }

    public DbSet<TranscriptionDbModel> Transcriptions { get; set; }

    public DbSet<VideoDbModel> Videos { get; set; }

    public DbSet<TranslationDbModel> Translations { get; set; }

    public DbSet<LanguageDbModel> Languages { get; set; }
}
