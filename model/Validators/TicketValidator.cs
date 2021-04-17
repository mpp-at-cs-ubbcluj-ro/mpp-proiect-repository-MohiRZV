using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.domain.Validators
{
    public class TicketValidator : Validator<Ticket>
    {
        public void validate(Ticket entity)
        {
            if (entity.client=="")
                throw new ValidationException("Numele clientului nu poate fi vid!");
            if (entity.festival == null)
                throw new ValidationException("Festivalul pentru care se vinde biletul nu poate fi null!");
        }
    }
}
