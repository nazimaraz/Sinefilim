using System;
using System.Collections.Generic;
using Data.Tables;

namespace Business.Interfaces
{
    public interface IPersonService
    {
        List<TitleCredit> GetTitleCredits(int titleId, int tmdbId);
    }
}
