using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPanel.Services
{
    public class JsonSaveService<T>
    {
        public static void Save(T obj, string fileName)
        {
            var str = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(fileName + ".json", str);
        }

        public static T Load(string fileName)
        { 
            if (!File.Exists(fileName + ".json"))
                File.WriteAllText(fileName + ".json", "");
            var jsonStr = File.ReadAllText(fileName + ".json");
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}
