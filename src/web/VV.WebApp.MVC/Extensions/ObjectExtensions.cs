using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace VV.WebApp.MVC.Extensions
{
    public static class ObjectExtensions
    {
        public static StringContent ToStringContent(this object objectToStringContent) =>
            new StringContent(JsonSerializer.Serialize(objectToStringContent), Encoding.UTF8, "application/json");
    }
}
