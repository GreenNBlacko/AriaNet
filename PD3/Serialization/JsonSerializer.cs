using Newtonsoft.Json;

namespace PD3.Serialization {
    public static class JsonSerializer<T> where T : class? {

        public static string ToJSON(T Object) => JsonConvert.SerializeObject(Object);
        
        public static T? FromJSON(string Data) => JsonConvert.DeserializeObject(Data) as T;
    }
}
