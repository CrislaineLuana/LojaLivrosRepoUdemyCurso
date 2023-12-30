using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace LojaLivros.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PerfilEnum
    {
        Administrador = 1,
        Operador = 2,
        Cliente = 0
        
    }
}
