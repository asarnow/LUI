using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace lasercom.extensions
{
    public static class LasercomExtensions
    {
        /// <summary>
        /// Returns all subclasses of the type. Note interfaces are not classes, but may have subclasses.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="concreteOnly"></param>
        /// <param name="excludeSystemTypes"></param>
        /// <returns></returns>
        public static List<Type> GetSubclasses(this Type type, bool concreteOnly = false, bool excludeSystemTypes = true)
        {
            List<Type> list = new List<Type>();
            IEnumerator enumerator = Thread.GetDomain().GetAssemblies().GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    Type[] types = ((Assembly)enumerator.Current).GetTypes();
                    if (!excludeSystemTypes || (excludeSystemTypes && !((Assembly)enumerator.Current).FullName.StartsWith("System.")))
                    {
                        IEnumerator enumerator2 = types.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            Type current = (Type)enumerator2.Current;
                            bool flag = (!current.IsInterface && (!concreteOnly || !current.IsAbstract));
                            flag &= type.IsInterface ? current.GetInterface(type.FullName) != null : current.IsSubclassOf(type);
                            if (flag) list.Add(current);
                        }
                    }
                }
                catch
                {
                }
            }
            return list;
        }
    }
}
