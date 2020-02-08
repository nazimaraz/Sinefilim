using Data.Tables;
using Data.Tables.TMDb.TitleDetails;

namespace Business.Interfaces
{
    public interface ITitleService
    {
        Title GetTitleDetails(int titleId);
        TitleDetailsResponse GetTitleDetailsFromTmdb(string imdbId);
        Title ParseTitle(TitleDetailsResponse titleDetails);
    }
}
