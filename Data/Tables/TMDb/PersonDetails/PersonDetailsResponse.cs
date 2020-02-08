using System;
namespace Data.Tables.TMDb.PersonDetails
{
    public class PersonDetailsResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        public string imdb_id { get; set; }
        public string profile_path { get; set; }
    }
}
