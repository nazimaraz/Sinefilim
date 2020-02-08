using System;
namespace Data.Tables.TMDb.GetTitleDetails
{
    public class TitleDetailsResponse
    {
        public string imdb_id { get; set; }
        public string original_title { get; set; }
        public string poster_path { get; set; }
        public int runtime { get; set; }
    }
}
