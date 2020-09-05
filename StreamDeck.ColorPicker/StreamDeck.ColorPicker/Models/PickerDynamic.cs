using BarRaider.SdTools;
using StreamDeck.ColorPicker.Helpers;

namespace StreamDeck.ColorPicker.Models
{
    internal class PickerDynamic : Picker
    {
        internal PickerDynamic(SDConnection connection, FormatFactory.ValueType valueType, bool copyToClipboard) : base(connection, valueType, copyToClipboard)
        {

        }

        internal override void OnTick()
        {
            mouseLocation = ScreenHelper.GetMouseLocation();
            SetImageKey();
        }
    }
}
