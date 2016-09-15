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


public class Rootobject
{
    public Result[] results { get; set; }
    public string status { get; set; }
}

public class Result
{
    public Address_Components[] address_components { get; set; }
    public string formatted_address { get; set; }
    public Geometry geometry { get; set; }
    public string place_id { get; set; }
    public string[] types { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
    public string location_type { get; set; }
    public Viewport viewport { get; set; }
}

public class Location
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Northeast
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Southwest
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Address_Components
{
    public string long_name { get; set; }
    public string short_name { get; set; }
    public string[] types { get; set; }
}
