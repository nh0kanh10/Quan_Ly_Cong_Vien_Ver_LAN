using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using ET;

namespace GUI
{
    public static class KanbanUIHelper
    {
        public static Guna2ShadowPanel CreateColumn(object caObj) // Phục hồi sau
        {
            // ET_CaLam ca = caObj as ET_CaLam;
            Guna2ShadowPanel pnlColumn = new Guna2ShadowPanel();
            pnlColumn.Size = new Size(280, 600);
            return pnlColumn;
        }

        public static Guna2Button CreateEmployeeCard(object pcObj, EventHandler onRemove) // Phục hồi sau
        {
            Guna2Button btn = new Guna2Button();
            return btn;
        }
    }
}
