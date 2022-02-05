using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChampionMasteryGg
{
    [JsonConverter(typeof(ChampionHighscoresDictionaryConverter)), DebuggerDisplay("Champions = {Count}")]
    public class ChampionHighscoresDictionary : ChampionMasteryGgDictionary<Champion, HighscoresCollection<PointsHighscore>>
    {
        internal ChampionHighscoresDictionary(IDictionary<Champion, HighscoresCollection<PointsHighscore>> dictionary) : base(dictionary)
        {
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ChampionHighscoresDictionaryConverter : JsonConverter<ChampionHighscoresDictionary>
    {
        public override ChampionHighscoresDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dict = JsonSerializer.Deserialize<Dictionary<int, HighscoresCollection<PointsHighscore>>>(ref reader, options: options);

            if (dict == null)
                return null;

            return (ChampionHighscoresDictionary)Activator.CreateInstance(
                typeToConvert,
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { dict.ToDictionary(kvp => new Champion { Id = kvp.Key }, kvp => kvp.Value) },
                culture: null);
        }

        public override void Write(Utf8JsonWriter writer, ChampionHighscoresDictionary dictionary, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, dictionary?.ToDictionary(kvp => kvp.Key.Id, kvp => kvp.Value), options);
    }
}