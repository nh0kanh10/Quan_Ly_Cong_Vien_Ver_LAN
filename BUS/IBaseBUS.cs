using System.Collections.Generic;
using ET;

namespace BUS
{
    public interface IBaseBUS<T> where T : class
    {
        List<T> LoadDS();
        ResponseResult Them(T et);
        ResponseResult Sua(T et);
        ResponseResult Xoa(int id);
        List<T> TimKiem(string kw, string filter);
    }
}
