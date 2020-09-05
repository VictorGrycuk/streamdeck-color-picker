﻿using BarRaider.SdTools;
using System.Drawing;

namespace StreamDeck.ColorPicker.Models
{
    internal class FormatHex : Format
    {
        internal override Font GetFont(string text)
        {
            return new Font("Consolas", 14, FontStyle.Bold);
        }

        internal override StringFormat GetStringFormat()
        {
            return new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        internal override string GetValueToCopy(Color color) => GetValueToShow(color);

        internal override string GetValueToShow(Color color)
        {
            return color.ToHex().Replace("#", string.Empty);
        }
    }
}