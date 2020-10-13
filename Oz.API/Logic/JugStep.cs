using Newtonsoft.Json;

namespace Oz.API.Logic
{
    public class JugStep
    {
        public JugStep(int firstAmount, int secondAmount)
        {
            FirstAmount = firstAmount;
            SecondAmount = secondAmount;
        }

        public int FirstAmount { get; }
        public int SecondAmount { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}