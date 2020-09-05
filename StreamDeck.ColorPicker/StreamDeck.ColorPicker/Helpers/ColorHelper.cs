using Newtonsoft.Json;
using StreamDeck.ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreamDeck.ColorPicker.Helpers
{
    public static class ColorHelper
    {
        internal static List<DefinitionColor> GetColorDefinition()
        {
            var definitionsPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "ColorDefinitions", "Colors.definition.json");
            return JsonConvert.DeserializeObject<List<DefinitionColor>>(File.ReadAllText(definitionsPath));
        }

        internal static string GetColorName(Color pixelColor, List<DefinitionColor> colorDefinitions)
        {
            var lowestDistance = 255;
            var colorName = string.Empty;

            foreach (var color in colorDefinitions)
            {
                var rgbDistance =
                    Math.Abs(pixelColor.R - color.Color.R) +
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

        internal static bool IsDarkColor(Color color)
        {
            var luminance = 0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B;
            return luminance < 0.179;
        }
    }
}
