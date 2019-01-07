using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NameAPI.Models;
using Newtonsoft.Json.Linq;

namespace NameAPI
{
    public class NameService
    {
        //Instansierar HttpClient klassen globalt för samtliga metoder i klassen
        static HttpClient client = new HttpClient();

        //Returnerar lista beroende på antal namn som valts
        public static async Task<List<NameModel>> GetNameList(int limit)
        {
            List<NameModel> nameList = new List<NameModel>();

            //Skickar request till API URIn med parametern limit
            HttpResponseMessage response = await client.GetAsync("http://api.namnapi.se/v2/names.json?limit=" + limit);

            //Tilldelar innehållet från anropet till content
            HttpContent content = response.Content;

            //Inväntar content, läser in som sträng och tildelar till data
            string data = await content.ReadAsStringAsync();

            //Använder JObject klassen från Newtonsoft paketet till att parsa strängen data till ett objekt
            dynamic dData = JObject.Parse(data);

            //Loopar igenom arrayen names i objektet dData och för varje instans av person skapas ny NameModel instans i listan som sedan returneras
            foreach (var person in dData.names)
            {
                nameList.Add(new NameModel { FirstName = person.firstname, LastName = person.surname, Gender = person.gender});
            }

            return nameList;
        }

        //Returnerar lista beroende av nameType och limit
        public static async Task<List<NameModel>> GetNameList(NameType? type, int limit)
        {
            List<NameModel> nameList = new List<NameModel>();

            string nameType = null;

            //Läser av parameter värden och skapa strängar som skickas med URIn
            if (type == NameType.Both) nameType = "both";
            if (type == NameType.FirstName) nameType = "firstname";
            if (type == NameType.SurName) nameType = "surname";

            HttpResponseMessage response = await client.GetAsync("http://api.namnapi.se/v2/names.json?type="
                                                                            + nameType + "&limit=" + limit);

            HttpContent content = response.Content;

            string data = await content.ReadAsStringAsync();

            dynamic dData = JObject.Parse(data);

            /*
             * Om nameType har värdet surname läggs Gender och FirstName propertyn inte till listan 
             * eftersom APIt inte skickar med någon gender eller firstname property när det anropas med surname
             */
            if (nameType == "surname")
            {
                foreach (var person in dData.names)
                {
                    nameList.Add(new NameModel { LastName = person.surname });
                }

                return nameList;
            }

            foreach (var person in dData.names)
            {
                nameList.Add(new NameModel { FirstName = person.firstname, LastName = person.surname, Gender = person.gender});
            }

            return nameList;
        }

        //Returnerar lista beroende på nameGender och limit
        public static async Task<List<NameModel>> GetNameList(NameGender? gender, int limit)
        {
            List<NameModel> nameList = new List<NameModel>();

            string nameGender = null;

            if (gender == NameGender.Both) nameGender = "both";
            if (gender == NameGender.Female) nameGender = "female";
            if (gender == NameGender.Male) nameGender = "male";

            HttpResponseMessage response = await client.GetAsync("http://api.namnapi.se/v2/names.json?gender="
                                                                            + nameGender + "&limit=" + limit);

            HttpContent content = response.Content;

            string data = await content.ReadAsStringAsync();

            dynamic dData = JObject.Parse(data);

            foreach (var person in dData.names)
            {
                nameList.Add(new NameModel { FirstName = person.firstname, LastName = person.surname, Gender = person.gender});
            }

            return nameList;
        }

        //Returnerar lista beroende på nameType, nameGender och limit
        public static async Task<List<NameModel>> GetNameList(NameType? type, NameGender? gender, int limit)
        {
            List<NameModel> nameList = new List<NameModel>();

            string nameGender = null;
            string nameType = null;

            if (gender == NameGender.Both) nameGender = "both";
            if (gender == NameGender.Female) nameGender = "female";
            if (gender == NameGender.Male) nameGender = "male";

            if (type == NameType.Both) nameType = "both";
            if (type == NameType.FirstName) nameType = "firstname";
            if (type == NameType.SurName) nameType = "surname";

            HttpResponseMessage response = await client.GetAsync("http://api.namnapi.se/v2/names.json?gender="
                                                       + nameGender + "&type=" + nameType + "&limit=" + limit);

            HttpContent content = response.Content;

            string data = await content.ReadAsStringAsync();

            dynamic dData = JObject.Parse(data);

            /*
             * Om nameType har värdet surname läggs Gender och FirstName propertyn inte till listan 
             * eftersom APIt inte skickar med någon gender eller firstname property när det anropas med surname
             */
            if (nameType == "surname")
            {
                foreach (var person in dData.names)
                {
                    nameList.Add(new NameModel { LastName = person.surname });
                }

                return nameList;
            }

            foreach (var person in dData.names)
            {
                nameList.Add(new NameModel { FirstName = person.firstname, LastName = person.surname, Gender = person.gender});
            }

            return nameList;
        }

    }
}
