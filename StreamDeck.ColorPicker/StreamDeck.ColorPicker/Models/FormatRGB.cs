using System;
using System.Drawing;

namespace StreamDeck.ColorPicker.Models
{
    internal class FormatRGB : Format
    {
        internal override Font GetFont(string text)
        {
            return new Font("Consolas", 120, FontStyle.Bold);
        }

        internal override StringFormat GetStringFormat()
        {
            return new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
        }

        internal override string GetValueToCopy(Color color)
        {
            return $"{ color.R },{ color.G },{ color.B }";
        }

        internal override string GetValueToShow(Color color)
        {
            return $"R:{ color.R + Environment.NewLine }G:{ color.G + Environment.NewLine }B:{ color.B }";
        }
    }
}
