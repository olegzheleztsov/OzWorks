using Newtonsoft.Json;

namespace Oz;

public class Data
{
    public Data(int value) => Value = value;
    public int Value { get; }

    public override string ToString() =>
        JsonConvert.SerializeObject(this);
}