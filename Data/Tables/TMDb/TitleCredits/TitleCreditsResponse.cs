using System;
using System.Collections.Generic;

namespace Data.Tables.TMDb.TitleCredits
{
    public class TitleCreditsResponse
    {
        public int id { get; set; }
        public List<TitleCreditsCast> cast { get; set; }
        public List<TitleCreditsCrew> crew { get; set; }
    }
}
