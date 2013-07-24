using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Helpers
{

    public static T CachedResultFunc<T>(string key, Func<T> func) {
        T value = (T)System.Web.Hosting.HostingEnvironment.Cache[key];
        if (value != null) {
            return value;
        }
        value = func();
        System.Web.Hosting.HostingEnvironment.Cache.Add(key, value,
            null, DateTime.Now.AddMinutes(1), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        return value;
    }

}
