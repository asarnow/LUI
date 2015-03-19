﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    public class GpibProviderFactory
    {

        public static IGpibProvider CreateGPIBProvider(GpibProviderParameters p)
        {
            return (IGpibProvider)System.Activator.CreateInstance(p.Type, p.ConstructorArray);
        }

        public static GpibProviderParameters CreateGPIBProviderParameters(GpibProviderParameters p)
        {
            GpibProviderParameters q = new GpibProviderParameters();
            q.Type = p.Type;
            q.Name = p.Name;
            q.PortName = p.PortName;
            q.Timeout = p.Timeout;
            q.BoardNumber = p.BoardNumber;
            return q;
        }

        public static void CopyParameters(GpibProviderParameters p, GpibProviderParameters q)
        {
            p.Type = q.Type;
            p.Name = q.Name;
            p.PortName = q.PortName;
            p.Timeout = q.Timeout;
            p.BoardNumber = q.BoardNumber;
        }

    }
}