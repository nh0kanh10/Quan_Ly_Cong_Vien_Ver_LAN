using System.Data.Linq;

namespace DAL
{
    public partial class DaiNamDBDataContext
    {
        partial void OnCreated()
        {

            this.DeferredLoadingEnabled = false;
        }
    }
}
