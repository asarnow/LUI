using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using log4net;

namespace lasercom.objects
{
    /// <summary>
    /// Base class for instrument-specific abstract classes.
    /// </summary>
    public abstract class LuiObject : ILuiObject
    {
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        abstract protected void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static ILuiObject Create<P>(LuiObjectParameters<P> p) where P:LuiObjectParameters<P>
        {
            return (ILuiObject)Activator.CreateInstance(p.Type, p);
        }

        public static ILuiObject Create(LuiObjectParameters p)
        {
            return (ILuiObject)Activator.CreateInstance(p.Type, p);
        }

        public static ILuiObject Create(LuiObjectParameters p, IEnumerable<ILuiObject> dependencies)
        {
            var args = (new object[] { p }).Concat(dependencies).ToArray();
            return (ILuiObject)Activator.CreateInstance(p.Type,
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding,
                    null, 
                    args,
                    CultureInfo.CurrentCulture);
        }

    }

    public abstract class LuiObject<P> : LuiObject where P : LuiObjectParameters<P>
    {   
        public abstract void Update(P p);
    }
}
