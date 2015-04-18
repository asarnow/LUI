using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace lasercom.objects
{
    /// <summary>
    /// Base class for instrument-specific abstract classes.
    /// </summary>
    public abstract class LuiObject : ILuiObject
    {
        abstract protected void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static ILuiObject Create<P>(LuiObjectParameters<P> p) where P:LuiObjectParameters<P>
        {
            return (ILuiObject)Activator.CreateInstance(p.Type, p.ConstructorArray);
        }

        public static ILuiObject Create(LuiObjectParameters p)
        {
            return (ILuiObject)Activator.CreateInstance(p.Type, p.ConstructorArray);
        }

        public static ILuiObject Create(LuiObjectParameters p, IEnumerable<ILuiObject> dependencies)
        {
            IEnumerable<object> args = p.ConstructorArray.AsEnumerable().Concat(dependencies);
            object[] ctorArgs = args.ToArray();
            return (ILuiObject)Activator.CreateInstance(p.Type,
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding,
                    null, 
                    ctorArgs,
                    CultureInfo.CurrentCulture);
        }
    }
}
