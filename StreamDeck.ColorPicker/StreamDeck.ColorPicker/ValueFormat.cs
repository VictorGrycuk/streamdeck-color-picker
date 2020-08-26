using BarRaider.SdTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreamDeck.ColorPicker
{
    public class ValueFormat
    {
        public Color Color { get; set; }
        public Font Font { get; set; }
        public SolidBrush TextBrush { get; set; }
        public StringFormat StringFormat { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public string ValueToShow { get; set; }
        public string ValueToCopy { get; set; }

        public ValueFormat(ValueType valueType, Color color)
        {
            Color = color;
            TextBrush = new SolidBrush(Color.Black);
            StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            XPosition = 72;
            YPosition = 72;

            // Value type specific properties
            switch (valueType)
            {
                case ValueType.ColorName:
                    Font = new Font("Consolas", 16, FontStyle.Bold);
                    ValueToShow = GetColorName(color);
                    ValueToCopy = ValueToShow;
                    break;
                case ValueType.RGBValue:
                    Font = new Font("Consolas", 16, FontStyle.Bold);
                    StringFormat.Alignment = StringAlignment.Near;
                    XPosition = 5;
                    ValueToShow = $"R:{ color.R + Environment.NewLine }G:{ color.G + Environment.NewLine }B:{ color.B }";
                    ValueToCopy = $"{ color.R },{ color.G },{ color.B }";
                    break;
                case ValueType.HexValue:
                    Font = new Font("Consolas", 14, FontStyle.Bold);
                    ValueToShow = color.ToHex().Replace("#", string.Empty);
                    ValueToCopy = ValueToShow;
                    break;
            }

            // Set the text color to white if the color is below certain threshold
            if ((color.R + color.G + color.B) / 3 < 60)
            {
                TextBrush.Color = Color.White;
            }
        }

        private static string GetColorName(Color pixelColor)
        {
            var colorsFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Colors.json");
            var colors = JsonConvert.DeserializeObject<List<NamedColor>>(File.ReadAllText(colorsFilePath));
            var lowestDistance = 256;
            var colorName = string.Empty;

            foreach (var color in colors)

            {
                var rgbDistance = Math.Abs(pixelColor.R - color.Color.R) +
                    Math.Abs(pixelColor.G - color.Color.G) +
                    Math.Abs(pixelColor.B - color.Color.B);

                if (rgbDistance <= lowestDistance)
                {
                    lowestDistance = rgbDistance;
                    colorName = color.Name;
                }
            }

            return colorName;
        }

        public class NamedColor
        {
            public string Name { get; set; }
            public Color Color { get; set; }
        }

        public enum ValueType
        {
            ColorName,
            RGBValue,
            HexValue,
        }
    }
}
