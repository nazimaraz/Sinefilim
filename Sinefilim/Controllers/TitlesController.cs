using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using Data.Tables;
using Data.Tables.TMDb.TitleDetails;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Sinefilim.Controllers
{
    [Route("api/[controller]")]
    public class TitlesController : Controller
    {
        private readonly ITitleService titleService;
        private readonly IPersonService personService;
        private readonly ITitleRepository<Title> titleRepository;
        private readonly ITitleRepository<TitleCredit> titleCreditRepository;

        public TitlesController(ITitleService titleService, IPersonService personService, ITitleRepository<Title> titleRepository, ITitleRepository<TitleCredit> titleCreditRepository)
        {
            this.titleService = titleService;
            this.personService = personService;
            this.titleRepository = titleRepository;
            this.titleCreditRepository = titleCreditRepository;
        }

        // GET: api/titles
        [HttpGet]
        public IQueryable<Title> Get()
        {
            return titleRepository.GetTable();
        }

        // POST api/titles
        [HttpPost]
        public Title Post([FromBody]JObject jObject)
        {
            string imdbId = (string)jObject["imdbId"];
            TitleDetailsResponse titleDetails = titleService.GetTitleDetailsFromTmdb(imdbId);
            Title title = titleService.ParseTitle(titleDetails);
            titleRepository.Add(title);
            List<TitleCredit> titleCredits = personService.GetTitleCredits(title.Id, title.TmdbId);
            titleCreditRepository.AddRange(titleCredits);
            return title;
        }
    }
}
