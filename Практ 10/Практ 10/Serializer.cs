using Newtonsoft.Json;

namespace Практ_10
{
    internal class Serializer
    {
        public static T? Load<T>(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (!File.Exists($"{path}\\{filename}"))
            {
                File.WriteAllText($"{path}\\{filename}", "");
            }

            string data = File.ReadAllText($"{path}\\{filename}");
            T? list = JsonConvert.DeserializeObject<T>(data);
            return list;
        }

        public static void Save<T>(T data, string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string serialized = JsonConvert.SerializeObject(data);
            File.WriteAllText($"{path}\\{filename}", serialized);
        }
    }
}
