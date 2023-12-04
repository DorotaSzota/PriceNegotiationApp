using System.Text.Json.Serialization;

namespace PriceNegotiationApp.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductCategory
{
    Electronics =1,
    Clothing =2,
    Home =3,
    Garden =4,
    Sports =5
}