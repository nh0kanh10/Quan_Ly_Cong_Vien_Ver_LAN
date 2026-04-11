using System;
using System.Linq;
using ET;
using DAL;

class Program {
    static void Main() {
        try {
            var et = DAL_KhuyenMai.Instance.LoadDS().FirstOrDefault(x => x.MaCode == "KMB27E6A");
            if (et != null) {
                et.GiaTriGiam = 10;
                DAL_KhuyenMai.Instance.Sua(et);
                Console.WriteLine("SUCCESS");
            } else {
                Console.WriteLine("NOT FOUND");
            }
        } catch (Exception ex) {
            Console.WriteLine("ERROR: " + ex.Message);
            if (ex.InnerException != null) Console.WriteLine("INNER: " + ex.InnerException.Message);
        }
    }
}
