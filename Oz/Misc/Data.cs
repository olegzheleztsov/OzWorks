using Newtonsoft.Json;

namespace Oz
{
    public class Data
    {
        public int Value { get; }

        public Data(int value) => Value = value;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}