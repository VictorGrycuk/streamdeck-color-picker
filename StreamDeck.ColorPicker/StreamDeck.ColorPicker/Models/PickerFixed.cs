using BarRaider.SdTools;
using StreamDeck.ColorPicker.Helpers;

namespace StreamDeck.ColorPicker.Models
{
    internal class PickerFixed : Picker
    {
        internal PickerFixed(SDConnection connection, FormatFactory.ValueType valueType, bool copyToClipboard) : base(connection, valueType, copyToClipboard)
        {

        }

        internal override void OnPress()
        {
            mouseLocation = ScreenHelper.GetMouseLocation();
        }

        internal override void OnTick()
        {
            SetImageKey();
            CopyToClipboard();
        }
    }
}
