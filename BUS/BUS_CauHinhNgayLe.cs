using System;
using System.Collections.Generic;
using ET;

namespace BUS
{
    public interface IBUS_CauHinhNgayLe
    {
        List<ET_CauHinhNgayLe> LoadDS();
        bool Them(ET_CauHinhNgayLe et);
        bool Sua(ET_CauHinhNgayLe et);
        bool Xoa(int id);
    }

    public class BUS_CauHinhNgayLe : IBUS_CauHinhNgayLe
    {
        private readonly ICauHinhNgayLeGateway _gateway;

        private static BUS_CauHinhNgayLe _instance;
        public static BUS_CauHinhNgayLe Instance => _instance ?? (_instance = new BUS_CauHinhNgayLe());

        private BUS_CauHinhNgayLe() : this(new DefaultCauHinhNgayLeGateway()) { }

        public BUS_CauHinhNgayLe(ICauHinhNgayLeGateway gateway)
        {
            _gateway = gateway;
        }

        public List<ET_CauHinhNgayLe> LoadDS()
        {
            return _gateway.LoadDS();
        }

        public bool Them(ET_CauHinhNgayLe et)
        {
            return _gateway.Them(et);
        }

        public bool Sua(ET_CauHinhNgayLe et)
        {
            return _gateway.Sua(et);
        }

        public bool Xoa(int id)
        {
            return _gateway.Xoa(id);
        }
    }
}
