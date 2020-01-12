using GovUkApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GovUkApi
{
    public interface IGovUkApi
    {
        Task<IEnumerable<User>> GetUsersInArea(Coords baseCoords = null, double milesWithin = 50.0);
    }
}