using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;

namespace WarmBeer.Core.Queries.Location
{
    public class LocationNameQuery : IAsyncRequestHandler<LocationNameQuery.Parameters, LocationNameQuery.Result>
    {
        public async Task<Result> Handle(Parameters message)
        {
            var latitude = message.Latitude.ToString(CultureInfo.InvariantCulture);
            var longitude = message.Longitude.ToString(CultureInfo.InvariantCulture);

            var url = $"http://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&sensor=true";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<Rootobject>(response);

                if (data.status.Equals("ZERO_RESULTS"))
                {
                    return new Result();
                }

                return new Result { Name = data.results[0].formatted_address };
            }
        }

        public class Parameters : IAsyncRequest<Result>
        {
            public Parameters(decimal longitude, decimal latitude)
            {
                Longitude = longitude;
                Latitude = latitude;
            }

            public decimal Longitude { get; private set; }

            public decimal Latitude { get; private set; }
        }

        public class Result
        {
            public string Name { get; set; }
        }
    }
}
