using System.Threading;

namespace StreamDeck.ColorPicker.Helpers
{
    public static class ClipboardHelper
    {
        internal static void SendToClipboard(string text)
        {
            var thread = new Thread(() => ClipboardThread(text));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = false;
            thread.Start();
        }

        private static void ClipboardThread(string text)
        {
            System.Windows.Forms.Clipboard.SetText(text);
        }
    }
}
