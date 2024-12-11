using Microsoft.AspNetCore.Mvc;
using YouTubeVideoProcessing.APIs.Common;
using YouTubeVideoProcessing.Infrastructure.Models;

namespace YouTubeVideoProcessing.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class TranslationFindManyArgs : FindManyInput<Translation, TranslationWhereInput> { }
