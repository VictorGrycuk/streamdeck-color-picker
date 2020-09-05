using StreamDeck.ColorPicker.Models;
using System;

namespace StreamDeck.ColorPicker
{
    internal static class FormatFactory
    {
        internal enum ValueType
        {
            ColorName,
            RGBValue,
            HexValue,
        }

        internal static Format GetFormat(ValueType valueType)
        {
            switch (valueType)
            {
                case ValueType.ColorName:
                    return new FormatName();
                case ValueType.RGBValue:
                    return new FormatRGB();
                case ValueType.HexValue:
                    return new FormatHex();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
