using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;

var timer = new PeriodicTimer(TimeSpan.FromHours(1));

while (await timer.WaitForNextTickAsync())
{

    string URL = "https://localhost:7284/";
    var client = new RestClient(URL);
    var req = new RestRequest("api/Position/GetAllPositions");
    var response = await client.ExecuteAsync(req);

    if (response.StatusCode == System.Net.HttpStatusCode.OK)
    {
        Console.WriteLine("Done");

    };

        
}




