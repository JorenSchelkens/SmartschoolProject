using System;
using Xunit;
using InventarisDomain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InventarisDomainTests
{
    public class LokaalTests
    {
        [Fact]
        public void JSONTest()
        {
            Lokaal lokaal = new Lokaal();

            Voorwerp temp = new Voorwerp(1, "bank");
            temp.aantal = 10;

            lokaal.Voorwerpen.Add(temp);

            temp = new Voorwerp(2, "stoel");
            temp.aantal = 5;

            lokaal.Voorwerpen.Add(temp);

            string jsonString = JsonConvert.SerializeObject(lokaal, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });

            Assert.NotNull(jsonString);
        }
    }
}