using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace lasercom.extensions
{
    /// <summary>
    /// Defines extension methods useful in lasercom and LUI.
    /// </summary>
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

        /// <summary>
        /// Topological sorting for dependency resolution.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes">Enumerable of nodes.</param>
        /// <param name="connected">Function returning enumerable over a node's children.</param>
        /// <returns></returns>
        public static IEnumerable<T> TopologicalSort<T>(this IEnumerable<T> nodes,
                                                Func<T, IEnumerable<T>> connected)
        {
            var elems = nodes.ToDictionary(node => node,
                                           node => new HashSet<T>(connected(node)));
            while (elems.Count > 0)
            {
                var elem = elems.FirstOrDefault(x => x.Value.Count == 0);
                if (elem.Key == null)
                {
                    throw new ArgumentException("Cyclic connections are not allowed");
                }
                elems.Remove(elem.Key);
                foreach (var selem in elems)
                {
                    selem.Value.Remove(elem.Key);
                }
                yield return elem.Key;
            }
        }
        
        public static void Raise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            var handler = eventHandler;
            if (handler != null) handler(sender, e);
        }

        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T:EventArgs
        {
            var handler = eventHandler;
            if (handler != null) handler(sender, e);
        }
    }
}
