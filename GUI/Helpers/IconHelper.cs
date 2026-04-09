using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace GUI
{
    public static class IconHelper
    {
        public static Bitmap GetBitmap(IconChar iconChar, Color color, int size)
        {
            return iconChar.ToBitmap(color, size);
        }

        public static IconChar GetIconFromMessageBox(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information: return IconChar.InfoCircle;
                case MessageBoxIcon.Error: return IconChar.TimesCircle;
                case MessageBoxIcon.Warning: return IconChar.ExclamationTriangle;
                case MessageBoxIcon.Question: return IconChar.QuestionCircle;
                default: return IconChar.Bell;
            }
        }

        public static Image LoadImage(string fileName)
        {
            try
            {
                string[] possiblePaths = {
                    System.IO.Path.Combine(Application.StartupPath, "pic", fileName),
                    System.IO.Path.Combine(System.IO.Directory.GetParent(Application.StartupPath).FullName, "pic", fileName),
                    System.IO.Path.Combine(System.IO.Directory.GetParent(Application.StartupPath).Parent.FullName, "pic", fileName),
                    System.IO.Path.Combine(System.IO.Directory.GetParent(Application.StartupPath).Parent != null && System.IO.Directory.GetParent(Application.StartupPath).Parent.Parent != null ? System.IO.Directory.GetParent(Application.StartupPath).Parent.Parent.FullName : "", "pic", fileName)
                };

                foreach (var path in possiblePaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        return Image.FromFile(path);
                    }
                }
            }
            catch { }
            return null;
        }
    }
}
