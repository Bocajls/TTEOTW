using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace UtilityLibrary
{
    public static class Extensions
    {
        public static T CopyObject<T>(this object objSource)
        {
            using MemoryStream stream = new();
            BinaryFormatter formatter = new();
            formatter.Serialize(stream, objSource);
            stream.Position = 0;

            return (T)formatter.Deserialize(stream);

        }

        public static T CreateDeepCopy<T>(T obj)
        {
            using var ms = new MemoryStream();
            XmlSerializer serializer = new(obj!.GetType());
            serializer.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(ms)!;
        }

        public static object DeepCopyJson(object o)
        {
            JsonSerializerOptions jsonOptions = new();
            jsonOptions.Converters.Add(new JsonStringEnumConverter());
            var json = JsonSerializer.Serialize(o, jsonOptions);
            return JsonSerializer.Deserialize(json, o.GetType(), jsonOptions)!;
        }

        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                msi.CopyTo(gs);
            }
            return Convert.ToBase64String(mso.ToArray());
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                gs.CopyTo(mso);
            }
            return Encoding.Unicode.GetString(mso.ToArray());
        }
    }
}
