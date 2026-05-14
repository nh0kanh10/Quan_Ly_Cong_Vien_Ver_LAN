using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using System;

namespace DaiNam.UITests.Core
{
    public static class UIHelper
    {
        /// <summary>
        /// Chờ cho đến khi element có thể click được và click
        /// </summary>
        public static void ClickWithRetry(Button button, int timeoutSeconds = 5)
        {
            Retry.WhileException(() => 
            {
                if (button != null && button.IsEnabled)
                {
                    button.Click();
                }
                else
                {
                    throw new Exception("Button not ready");
                }
            }, TimeSpan.FromSeconds(timeoutSeconds), TimeSpan.FromMilliseconds(200));
        }

        /// <summary>
        /// Nhập text với Retry an toàn
        /// </summary>
        public static void EnterTextWithRetry(TextBox textBox, string text, int timeoutSeconds = 5)
        {
            Retry.WhileException(() =>
            {
                if (textBox != null && textBox.IsEnabled)
                {
                    textBox.Text = text;
                }
                else
                {
                    throw new Exception("TextBox not ready");
                }
            }, TimeSpan.FromSeconds(timeoutSeconds), TimeSpan.FromMilliseconds(200));
        }
    }
}
