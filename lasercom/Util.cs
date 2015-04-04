using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using log4net;
using System.Threading;
using System.Reflection;
using System.Collections;

namespace lasercom
{
    /// <summary>
    /// Provides a variety of utility methods.
    /// </summary>
    public static class Util
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Might throw IOException
        public static List<string> ReadTimesFile(String filename)
        {
            List<string> times = new List<string>();
            foreach (string line in ReadLines(filename))
            {
                line.Trim();
            }
            return times;
        }

        // <summary>
        // Throws IOException.
        // </summary>
        public static IEnumerable<string> ReadLines(string filename)
        {
            using (TextReader tr = new StreamReader(filename))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        [DllImport("kernel32.dll")]
        static extern uint QueryDosDevice(string lpDeviceName, IntPtr lpTargetPath, uint ucchMax);

        public static List<string> EnumerateSerialPorts()
        {
            int ERROR_INSUFFICIENT_BUFFER = 122;
            // Allocate some memory to get a list of all system devices.
            // Start with a small size and dynamically give more space until we have enough room.
            int returnSize = 0;
            int maxSize = 100;
            string allDevices = null;
            IntPtr mem;
            string[] retval = null;

            while (returnSize == 0)
            {
                mem = Marshal.AllocHGlobal(maxSize);
                if (mem != IntPtr.Zero)
                {
                    // mem points to memory that needs freeing
                    try
                    {
                        returnSize = (int)QueryDosDevice(null, mem, (uint)maxSize);
                        if (returnSize != 0)
                        {
                            allDevices = Marshal.PtrToStringAnsi(mem, returnSize);
                            retval = allDevices.Split('\0');
                            break;    // not really needed, but makes it more clear...
                        }
                        else if (Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
                        {
                            maxSize *= 10;
                        }
                        else
                        {
                            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(mem);
                    }
                }
                else
                {
                    throw new OutOfMemoryException();
                }
            }

            List<string> ports = new List<string>();

            foreach (string device in retval) if (device.Contains("COM")) ports.Add(device);

            return ports;
        }

        /// <summary>
        /// Simple Bernstein hash. Use to combine hash codes of multiple objects.
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="h2"></param>
        /// <returns></returns>
        public static int Hash(int h1, int h2)
        {
            unchecked { return (h1 << 5) * h2; } // Hash should wrap around.
        }

        public static int Hash(int h1, object o)
        {
            if (o == null) return h1;
            return Hash(h1, o.GetHashCode());
        }

        public static int Hash(object o1, object o2)
        {
            if (o1 == null && o2 == null)
            {
                return 0;
            } else if (o1 != null)
            {
                return o1.GetHashCode();
            }
            else if (o2 != null)
            {
                return o2.GetHashCode();
            }
            else
            {
                return Hash(o1.GetHashCode(), o2.GetHashCode());
            }
        }

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
    }
}
