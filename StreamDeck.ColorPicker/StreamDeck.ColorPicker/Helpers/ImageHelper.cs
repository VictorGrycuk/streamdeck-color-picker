using BarRaider.SdTools;
using StreamDeck.ColorPicker.Models;
using System.Drawing;

namespace StreamDeck.ColorPicker.Helpers
{
    public static class ImageHelper
    {
        internal static Image GetImage(Color color)
        {
            var bmp = Tools.GenerateGenericKeyImage(out Graphics graphics);
            graphics.FillRectangle(new SolidBrush(color), 0, 0, bmp.Width, bmp.Height);

            return bmp;
        }

        internal static Font ResizeFont(Graphics graphics, string text, Font font)
        {
            var newSize = graphics.MeasureString(text, font);
            if (newSize.Width > 142)
            {
                return ResizeFont(graphics, text, new Font("Consolas", font.Size - 2, FontStyle.Bold, GraphicsUnit.Pixel));
            }

            return font;
        }

        internal static Image SetImageText(Image image, string text, Format format, bool isDarkColor)
        {
            var font = format.GetFont(text);
            var stringFormat = format.GetStringFormat();
            var isRGB = stringFormat.Alignment == StringAlignment.Near;

            using (var graphics = Graphics.FromImage(image))
            {
                if (!isRGB) font = ResizeFont(graphics, text, font);
                graphics.DrawString(text, font, isDarkColor ? Brushes.White : Brushes.Black, !isRGB ? 72 : 5 , 72, stringFormat);
            }

            return image;
        }
    }
}
