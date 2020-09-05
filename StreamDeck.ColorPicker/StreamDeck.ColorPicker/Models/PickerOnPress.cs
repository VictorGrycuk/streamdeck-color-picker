using BarRaider.SdTools;
using StreamDeck.ColorPicker.Helpers;

namespace StreamDeck.ColorPicker.Models
{
    internal class PickerOnPress : Picker
    {
        internal PickerOnPress(SDConnection connection, FormatFactory.ValueType valueType, bool copyToClipboard) : base(connection, valueType, copyToClipboard)
        {
            
        }

        internal override void OnPress()
        {
            mouseLocation = ScreenHelper.GetMouseLocation();
            SetImageKey();
        }
    }
}
