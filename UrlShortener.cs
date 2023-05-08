using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace UrlShortener;

public class UrlModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement("OriginalUrl")]
    public string OriginalUrl { get; set; }
    [BsonElement("ShortenedUrl")]
    public string ShortenedUrl { get; set; }
}


public static class UrlShortener
{
    [FunctionName("UrlShortener")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req, ILogger log)
    {
        var client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));
        var database = client.GetDatabase("UrlShortener");
        var collection = database.GetCollection<UrlModel>("urls");

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UrlModel>(requestBody);
            string shortenedUrl = req.Query["short"];
            string originalUrl = req.Query["original"];

            if (string.IsNullOrEmpty(shortenedUrl) && string.IsNullOrEmpty(originalUrl)
                  || string.IsNullOrWhiteSpace(shortenedUrl) && string.IsNullOrWhiteSpace(originalUrl))
            {
                return new BadRequestObjectResult("Please enter a url");
            }

            if (string.IsNullOrEmpty(originalUrl))
            {
                var filter = Builders<UrlModel>.Filter
                    .Eq(r => r.ShortenedUrl, shortenedUrl);

                var projection = Builders<UrlModel>.Projection.Exclude(r => r.Id);

                var result = await collection.Find(filter).Project(projection).FirstOrDefaultAsync();

                return new OkObjectResult(result[0].AsBsonValue);

            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            shortenedUrl = new string(stringChars);

            var newUrl = new UrlModel()
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl
            };

            await collection.InsertOneAsync(newUrl);

            return new OkObjectResult(shortenedUrl);

        }
        catch (JsonException ex)
        {
            log.LogError(ex, "Failed to deserialize request body to UrlModel");
            return new BadRequestObjectResult("Invalid request body");
        }
        catch (Exception ex)
        {
            log.LogError(ex, "An error occurred while processing the request");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}