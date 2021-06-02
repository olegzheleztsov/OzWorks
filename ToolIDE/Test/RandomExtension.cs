#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

#endregion

namespace ToolIDE.Test
{
    public class RandomExtension : MarkupExtension
    {
        private static readonly Random Rnd = new();
        private readonly int _from, _to;
        
        public bool UseFractions { get; set; }

        public RandomExtension(int from, int to)
        {
            _from = from;
            _to = to;
        }

        public RandomExtension(int to)
            : this(0, to)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var value = UseFractions ? Rnd.NextDouble() * (_to - _from) + _from : Rnd.Next(_from, _to);
            Type targetType = null;
            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
            {
                var clrProp = target.TargetProperty as PropertyInfo;
                if (clrProp != null)
                {
                    targetType = clrProp.PropertyType;
                }

                if (targetType == null)
                {
                    if (target.TargetProperty is DependencyProperty dp)
                    {
                        targetType = dp.PropertyType;
                    }
                }
            }

            return targetType != null ? Convert.ChangeType(value, targetType) : value.ToString();
        }
    }
}