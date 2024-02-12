using BarRaider.SdTools;
using StreamDeck.ColorPicker.Models;
using StreamDeck.ColorPicker.Settings;
using System;

namespace StreamDeck.ColorPicker
{
    internal enum FunctionType
    {
        OnKeyPress,
        Dynamic,
        Fixed
    }

    internal static class ColorPickerFactory
    {
        internal static Picker GetColorPicker(SDConnection connection, PickerSettings settings)
        {
            switch (settings.FunctionType)
            {
                case FunctionType.OnKeyPress:
                    return new PickerOnPress(connection, settings.ValueToShow, settings.CopyToClipboard);
                case FunctionType.Dynamic:
                    return new PickerDynamic(connection, settings.ValueToShow, settings.CopyToClipboard);
                case FunctionType.Fixed:
                    return new PickerFixed(connection, settings.ValueToShow, settings.CopyToClipboard);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
