using System;

namespace GovUKMain
{
    using GovUkApi;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IGovUkApi api = new GovUkApi();

                var londonUsers = api.GetUsersInArea().Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
