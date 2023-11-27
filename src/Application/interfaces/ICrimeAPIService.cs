using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface ICrimeAPIService
    {
        Task<missingPerson> returnMissingPersonData();



        Task<string> addMissingPerson(missingPerson person);
        Task removeMissingPerson(missingPerson person);
        Task updateMissingPerson(missingPerson person);
        Task<List<missingPerson>> ReturnMissingdPersonData();

      
        Task addStolenItem(stolenItem item);
        Task removeStolenItem(stolenItem item);
        Task updateStolenItem(stolenItem item);
        Task<stolenItem> returnStolenItem();





        Task addWantedPersonData(wantedPerson person);
        Task removeWantedPersonData(wantedPerson person);
        Task updateWantedPersonData(wantedPerson person);

        Task<wantedPerson> returnWantedPersonData();


    }
}
