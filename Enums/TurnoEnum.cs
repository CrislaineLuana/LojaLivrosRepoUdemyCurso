using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace LojaLivros
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TurnoEnum
    {
        Manha = 1,
        Tarde = 2,
        Madrugada = 3,
        Cliente = 4
    }
}
