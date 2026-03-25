using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ET_DanhMucDV
    {
        private string tenDanhMuc, kieuLogic, moTa, icon;
        private int maDanhMuc;
        public ET_DanhMucDV() { }
        public ET_DanhMucDV(int maDanhMuc, string tenDanhMuc, string kieuLogic, string moTa, string icon)
        {
            this.MaDanhMuc = maDanhMuc;
            this.TenDanhMuc = tenDanhMuc;
            this.KieuLogic = kieuLogic;
            this.MoTa = moTa;
            this.Icon = icon;
        }

        public string TenDanhMuc { get => tenDanhMuc; set => tenDanhMuc = value; }
        public string KieuLogic { get => kieuLogic; set => kieuLogic = value; }
        public string MoTa { get => moTa; set => moTa = value; }
        public string Icon { get => icon; set => icon = value; }
        public int MaDanhMuc { get => maDanhMuc; set => maDanhMuc = value; }
    }
}
