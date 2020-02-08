using System;
using Business.Interfaces;
using Data.Tables;
using System.Net;
using Newtonsoft.Json;
using Data.Tables.TMDb.TmdbId;
using Data.Tables.TMDb.TitleDetails;

namespace Business.Services
{
    public class TitleService : ITitleService
    {
        private readonly string apiKey = "8c4cf47ef096c479b39ff7423eb1d6b6";
        private int tmdbId;

        public TitleService()
        {
        }

        public Title GetTitleDetails(int titleId)
        {
            return new Title { Id = 1, Name = "Test", PosterUrl = "PosterUrl" };
        }

        public TitleDetailsResponse GetTitleDetailsFromTmdb(string imdbId)
        {
            tmdbId = GetTmdbId(imdbId);
            string url = $"https://api.themoviedb.org/3/movie/{tmdbId}?api_key={apiKey}&language=us-US";
            using WebClient webClient = new WebClient();
            string json_data = string.Empty;
            json_data = webClient.DownloadString(url);
            TitleDetailsResponse response = JsonConvert.DeserializeObject<TitleDetailsResponse>(json_data);
            return response;
        }

        public Title ParseTitle(TitleDetailsResponse titleDetails)
        {
            return new Title
            {
                ImdbId = titleDetails.imdb_id,
                TmdbId = tmdbId,
                Name = titleDetails.original_title,
                PosterUrl = titleDetails.poster_path,
                Runtime = titleDetails.runtime 
            };
        }

        private int GetTmdbId(string imdbUrl)
        {
            string url = $"https://api.themoviedb.org/3/find/{imdbUrl}?api_key={apiKey}&language=en-US&external_source=imdb_id";
            using WebClient webClient = new WebClient();
            string json_data = string.Empty;
            json_data = webClient.DownloadString(url);
            TmdbIdResponse response = JsonConvert.DeserializeObject<TmdbIdResponse>(json_data);
            return response.movie_results[0].id;
        }
    }
}
