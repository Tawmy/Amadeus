using Anilist4Net.Enums;

namespace Amadeus.Bot.Helper;

public static class AniListHelper
{
    public static string GetStatusString(MediaStatuses status)
    {
        return status switch
        {
            MediaStatuses.NOT_YET_RELEASED => "Not yet released",
            MediaStatuses.RELEASING => "Releasing",
            MediaStatuses.FINISHED => "Finished",
            MediaStatuses.HIATUS => "On hiatus",
            MediaStatuses.CANCELLED => "Cancelled",
            _ => string.Empty
        };
    }
}