using Connector.Factories;
using Connector.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Connector.API;

public static class RateEndpoints
{
    
    public static void MapRateEndpoints (this IEndpointRouteBuilder routes)
    {
        RateList _rateList = RatesListFactory.GetRateList();
      
        var group = routes.MapGroup("/api/Rate").WithTags(nameof(Rate));

        group.MapGet("/", () =>
        {
            var list = new List<Rate>();

            _rateList.GetRates().ForEach(rate => {
                dynamic json = JsonConvert.DeserializeObject(rate.Value);
                var tmpRate = new Rate();
                tmpRate.ID = rate.ID;
                tmpRate.Code = rate.Code;
                tmpRate.Valid = rate.Valid;
                tmpRate.Value = rate.Value;
                list.Add(tmpRate);
            });

            return list;
        })
        .WithName("GetAllRates")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            return _rateList.GetRateById(id);
        })
        .WithName("GetRateById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Rate input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateRate")
        .WithOpenApi();

        group.MapPost("/", (Rate model) =>
        {
            model.ID = _rateList.GetRates().Count() + 1;
            _rateList.GetRates().Add(model);
            return TypedResults.Created($"/api/Rates/{model.ID}", model);
        })
        .WithName("CreateRate")
        .WithOpenApi();

    }
}
