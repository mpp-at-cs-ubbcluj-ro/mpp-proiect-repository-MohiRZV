using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.domain.Validators
{
    class FestivalValidator : Validator<Festival>
    {
        public void validate(Festival entity)
        {
            if (entity.name=="" || entity.genre=="" || entity.location=="")
                throw new ValidationException("Numele, genreul si locatia nu pot fi vide!");
            if (entity.seats <= 0)
                throw new ValidationException("Festivalul trebuie sa aiba locuri disponibile!");
        }
    }
}
