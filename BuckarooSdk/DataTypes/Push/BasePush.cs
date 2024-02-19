using static BuckarooSdk.Constants.Services;
using System.Collections.Generic;

namespace BuckarooSdk.DataTypes.Push
{
    public abstract class BasePush
    {
        public abstract List<ServiceNames> GetServices();
    }
}
