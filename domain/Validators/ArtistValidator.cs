using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.domain.Validators
{
    class ArtistValidator : Validator<Artist>
    {
        public void validate(Artist entity)
        {
            if (entity.name=="" || entity.genre=="")
                throw new ValidationException("Numele si genreul nu pot fi vide!");
        }
    }
}
