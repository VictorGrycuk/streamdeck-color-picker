using StreamDeck.ColorPicker.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace StreamDeck.ColorPicker.Models
{
    internal class FormatName : Format
    {
        private readonly List<DefinitionColor> definitionColors;

        public FormatName()
        {
            definitionColors = ColorHelper.GetColorDefinition();
        }

        internal override Font GetFont(string text)
        {
            // Based on what I think look best three letter names, which is around 142 px
            var fontSize = 70;

            return new Font("Consolas", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
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
            return ColorHelper.GetColorName(color, definitionColors).Replace(" ", Environment.NewLine);
        }
    }
}
