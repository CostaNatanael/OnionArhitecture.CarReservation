using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnionArhitecture.CarReservation.API.Converters
{
    //internal class DateTimeConverter : JsonConverter<DateTime>
    //{

    //    private readonly string Format= "MMM dd, yyyy H:mm:ss tt zzz";
      
    //    public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
    //    {
    //        var offset = TimeZoneInfo.Local.GetUtcOffset(date);
    //        string json = JsonConvert.SerializeObject(date, Formatting.Indented);
    //        Console.WriteLine("{0} --- {1}", json, offset);
           

    //        writer.WriteStringValue(date.ToString(Format));
    //    }
    //    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        return DateTime.ParseExact(reader.GetString(), Format, null);
    //    }
    //}
}
