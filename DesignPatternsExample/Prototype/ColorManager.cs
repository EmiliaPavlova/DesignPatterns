namespace Prototype
{
    using System.Collections.Generic;

    public class ColorManager
    {
        private Dictionary<string, ColorPrototype> colors = new Dictionary<string, ColorPrototype>();

        public ColorPrototype this[string key]
        {
            get { return colors[key]; }
            set { colors.Add(key, value); }
        }
    }
}
