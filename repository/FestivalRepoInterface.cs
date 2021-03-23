using LabMPP.domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabMPP.repository
{
    interface FestivalRepoInterface:RepositoryInterface<long,Festival>
    {

        IEnumerable<Festival> FindByDate(DateTime date);
    }
}
