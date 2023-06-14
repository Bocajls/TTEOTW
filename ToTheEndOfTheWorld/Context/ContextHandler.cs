using ModelLibrary.Concrete;
using Newtonsoft.Json;
using System.IO;

namespace ModelLibrary.Context
{
    public class ContextHandler
    {
        private static string BasePath => $@"{Path.GetTempPath()}\TTEOTW\";

        public ContextHandler()
        {
            if (Directory.Exists(BasePath)) return;
            
            Directory.CreateDirectory(BasePath);
        }

        public static void SaveWorld(World world)
        {
            var file = $"{BasePath}World.txt";
            CreateDirectoryIfNotExists(file);
            CreateFileIfNotExists(file);
            File.WriteAllText(
                file,
                UtilityLibrary.Extensions.Compress(
                    JsonConvert.SerializeObject(
                        world,
                        typeof(World),
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        }
                    )
                )
            );
        }


        public static World LoadWorld()
        {
            var file = $"{BasePath}World.txt";
            CreateDirectoryIfNotExists(file);
            CreateFileIfNotExists(file);
            return JsonConvert.DeserializeObject<World>(
                UtilityLibrary.Extensions.Decompress(
                    File.ReadAllText(file)
                ),
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                }
            )!;
        }


        private static void CreateDirectoryIfNotExists(string file)
        {
            if (Directory.Exists(Path.GetDirectoryName(file))) return;
            Directory.CreateDirectory(Path.GetDirectoryName(file));
        }

        private static void CreateFileIfNotExists(string file)
        {
            if (File.Exists(file)) return;
            using var f = File.Create(file);
            f.Close();
        }
    }
}