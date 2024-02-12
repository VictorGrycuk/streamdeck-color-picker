using System.Drawing;

namespace StreamDeck.ColorPicker.Models
{
    abstract internal class Format
    {
        internal abstract string GetValueToShow(Color color);

        internal abstract string GetValueToCopy(Color color);
        internal abstract Color GetColorFromString(string color);

        internal abstract Font GetFont(string text);

        internal abstract StringFormat GetStringFormat();
    }
}
