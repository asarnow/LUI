using HDF5DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.io
{
    public abstract class MatVar : IDisposable
    {
        public string Name;

        protected abstract void Close();

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class MatVar<T> : MatVar
    {
        private readonly H5DataTypeId TypeId;
        private H5DataSpaceId SpaceId;
        private H5DataSetId DataSetId;
        public long[] Dims;
        public long[] Cursor { get; set; }

        protected internal MatVar(string _Name, H5FileOrGroupId FileOrGroupId, params long[] _Dims)
        {
            string MatlabClass;

            if (typeof(T) == typeof(int))
            {
                TypeId = H5T.copy(H5T.H5Type.STD_I32LE);
                MatlabClass = "int32";
            } 
            else if (typeof(T) == typeof(double))
            {
                TypeId = H5T.copy(H5T.H5Type.IEEE_F64LE);
                MatlabClass = "double";
            }
            else
            {
                throw new ArgumentException();
            }

            Name = _Name;
            Dims = _Dims;
            Cursor = new long[Dims.Length];

            SpaceId = H5S.create_simple(Dims.Length, Dims);
            DataSetId = H5D.create(FileOrGroupId, "/" + Name, TypeId, SpaceId);
            H5DataTypeId AttributeTypeId = H5T.create(H5T.CreateClass.STRING, MatlabClass.Length);
            H5DataSpaceId AttributeSpaceId = H5S.create(H5S.H5SClass.SCALAR);
            H5AttributeId AttributeId = H5A.create(DataSetId, "MATLAB_class", AttributeTypeId, AttributeSpaceId);
            byte[] asciiBytes = Encoding.ASCII.GetBytes(MatlabClass);
            H5A.write(AttributeId, AttributeTypeId, new H5Array<byte>(asciiBytes));
            H5A.close(AttributeId);
            H5S.close(AttributeSpaceId);
            H5T.close(AttributeTypeId);
        }

        /// <summary>
        /// Write data along one dimension of the array using the cursor.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dim"></param>
        public void WriteNext(T[] data, long dim)
        {
            long[] count = Enumerable.Repeat(1L, Dims.Length).ToArray(); // Ones.
            for (int i = 0; i < Dims.Length; i++)
                if (i != dim) count[i] = Dims[i];

            long RequiredLength = 1;
            foreach (long l in count) RequiredLength *= l;

            if (data.Length != RequiredLength)
                throw new ArgumentException("Data size must match array dimension");

            long[] start = new long[Dims.Length];
            start[dim] = Cursor[dim];

            H5S.selectHyperslab(SpaceId, H5S.SelectOperator.SET, start, count);
            H5DataSpaceId memSpaceId = H5S.create_simple(count.Length, count);
            H5PropertyListId propListId = H5P.create(H5P.PropertyListClass.DATASET_XFER);
            H5D.write(DataSetId, TypeId, memSpaceId, SpaceId, propListId, new H5Array<T>(data));
            H5S.close(memSpaceId);
            Cursor[dim]++;
            for (int i = 0; i < Cursor.Length; i++)
                if (i != dim) Cursor[i] = 0;
        }

        /// <summary>
        /// Write data into the variable's HDF5 data set in row-major order. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public void Write(T[] data, long[] start, long[] count)
        {
            H5S.selectHyperslab(SpaceId, H5S.SelectOperator.SET, start, count);
            H5DataSpaceId memSpaceId = H5S.create_simple(count.Length, count);
            H5PropertyListId propListId = H5P.create(H5P.PropertyListClass.DATASET_XFER);
            H5D.write(DataSetId, TypeId, memSpaceId, SpaceId, propListId, new H5Array<T>(data));
            H5S.close(memSpaceId);
        }

        protected override void Close()
        {
            H5D.close(DataSetId);
            H5S.close(SpaceId);
            H5T.close(TypeId);
        }
    }
}
