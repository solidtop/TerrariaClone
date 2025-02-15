using System.IO;
using Newtonsoft.Json;

namespace TerrariaClone.Common.Utilities
{
    public abstract class ConfigLoader<T>
    {
        public static T Load(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}


