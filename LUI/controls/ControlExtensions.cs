
namespace LUI.controls
{
    public static class ControlExtensions
    {
        public static void AutoResize(this System.Windows.Forms.TextBox self)
        {
            var size = System.Windows.Forms.TextRenderer.MeasureText(self.Text, self.Font);
            if (self.MinimumSize.Height > size.Height) size.Height = self.MinimumSize.Height;
            if (self.MinimumSize.Width > size.Width) size.Width = self.MinimumSize.Width;
            self.Size = size;
        }
    }
}
