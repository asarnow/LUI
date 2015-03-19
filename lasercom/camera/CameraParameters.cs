using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom;
using lasercom.objects;

namespace lasercom.camera
{
    public class CameraParameters : LuiObjectParameters
    {

        public string Dir { get; set; }
        public int Temperature { get; set; }

        override public object[] ConstructorArray
        {
            get
            {
                object[] arr = null;
                if (Type == typeof(AndorCamera))
                {
                    arr = new object[] { Dir };
                }
                else if (Type == typeof(CameraTempControlled))
                {
                    arr = new object[] { Dir, Temperature };
                }
                return arr;
            }
        }

        public CameraParameters(Type Type)
            : base(Type)
        {

        }

        public CameraParameters()
            : base()
        {

        }
    }
}
