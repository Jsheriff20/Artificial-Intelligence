﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var floodRiskAssesment = FloodRiskAssesment.FromJson(jsonString);

namespace flood_risk_analysis_namespace
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FloodRiskAssesment
    {
        [JsonProperty("@context")]
        public Uri Context { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("eaAreaName")]
        public string EaAreaName { get; set; }

        [JsonProperty("fwdCode")]
        public string FwdCode { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("long")]
        public double Long { get; set; }

        [JsonProperty("notation")]
        public string Notation { get; set; }

        [JsonProperty("polygon")]
        public Uri Polygon { get; set; }

        [JsonProperty("quickDialNumber")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long QuickDialNumber { get; set; }

        [JsonProperty("riverOrSea")]
        public string RiverOrSea { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("licence")]
        public Uri Licence { get; set; }

        [JsonProperty("documentation")]
        public Uri Documentation { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("hasFormat")]
        public List<Uri> HasFormat { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }
    }

    public partial class FloodRiskAssesment
    {
        public static FloodRiskAssesment FromJson(string json) => JsonConvert.DeserializeObject<FloodRiskAssesment>(json, flood_risk_analysis_namespace.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this FloodRiskAssesment self) => JsonConvert.SerializeObject(self, flood_risk_analysis_namespace.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}