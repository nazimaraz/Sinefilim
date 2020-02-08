using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Business.Interfaces;
using Data.Tables;
using Data.Tables.TMDb.TitleCredits;
using Data.Tables.TMDb.PersonDetails;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly string apiKey = "8c4cf47ef096c479b39ff7423eb1d6b6";
        private readonly ITitleRepository<Person> personRepository;

        public PersonService(ITitleRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        public List<TitleCredit> GetTitleCredits(int titleId, int tmdbId)
        {
            string url = $"https://api.themoviedb.org/3/movie/{tmdbId}/credits?api_key={apiKey}";
            using WebClient webClient = new WebClient();
            string jsonData = string.Empty;
            jsonData = webClient.DownloadString(url);
            TitleCreditsResponse response = JsonConvert.DeserializeObject<TitleCreditsResponse>(jsonData);
            List<TitleCredit> titleCreditList = new List<TitleCredit>();
            foreach (TitleCreditsCast castPerson in response.cast)
            {
                Person person = personRepository.Where(person => person.TmdbId == castPerson.id).FirstOrDefault();
                if(person == null) {
                    person = GetPersonDetailsFromTmdb(castPerson.id);
                    personRepository.Add(person);
                }
                TitleCredit titleCredit = new TitleCredit
                {
                    PersonId = person.Id,
                    TitleId = titleId,
                    TmdbId = castPerson.id,
                    TmdbCreditId = castPerson.credit_id,
                    Character = castPerson.character,
                    Gender = castPerson.gender,
                    Name = castPerson.name,
                    Order = castPerson.order,
                    PosterUrl = castPerson.profile_path,
                    Department = "",
                    Job = ""
                };
                titleCreditList.Add(titleCredit);
            }

            foreach (TitleCreditsCrew crewPerson in response.crew)
            {
                Person person = personRepository.Where(person => person.TmdbId == crewPerson.id).FirstOrDefault();
                if(person == null) {
                    person = GetPersonDetailsFromTmdb(crewPerson.id);
                    personRepository.Add(person);
                }
                TitleCredit titleCredit = new TitleCredit
                {
                    PersonId = person.Id,
                    TitleId = titleId,
                    TmdbId = crewPerson.id,
                    TmdbCreditId = crewPerson.credit_id,
                    Character = "",
                    Gender = crewPerson.gender,
                    Name = crewPerson.name,
                    Order = -1,
                    PosterUrl = crewPerson.profile_path,
                    Department = "",
                    Job = ""
                };
                titleCreditList.Add(titleCredit);
            }

            return titleCreditList;
        }

        private Person GetPersonDetailsFromTmdb(int tmdbId)
        {
            string url = $"https://api.themoviedb.org/3/person/{tmdbId}?api_key={apiKey}&language=en-US";
            using WebClient webClient = new WebClient();
            string jsonData = string.Empty;
            jsonData = webClient.DownloadString(url);
            PersonDetailsResponse response = JsonConvert.DeserializeObject<PersonDetailsResponse>(jsonData);
            return new Person
            {
                TmdbId = response.id,
                ImdbId = response.imdb_id,
                Gender = response.gender,
                Name = response.name,
                PosterUrl = response.profile_path
            };
        }
    }
}
