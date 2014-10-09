using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HDF5DotNet;

namespace LUI.io
{
    public class MatFile:IDisposable
    {

        private readonly string _FileName;
        public string FileName
        {
            get
            {
                return _FileName;
            }
        }
        private readonly H5FileId FileId;
        private readonly H5GroupId GroupId;
        private readonly H5DataTypeId TypeId;
        private H5DataSpaceId SpaceId;
        private H5DataSetId DataSetId;
        private int M, N;

        public int RowCursor { get; set; }
        public int ColCursor { get; set; }

        public MatFile(string fileName, string matlabClass, int m, int n)
        {
            _FileName = fileName;
            FileId = H5F.create(FileName, H5F.CreateMode.ACC_TRUNC);
            GroupId = H5G.open(FileId, "/");

            string varname = "data";

            switch (matlabClass)
            {
                case "int32":
                    TypeId = H5T.copy(H5T.H5Type.STD_I32LE);
                    break;
                case  "double":
                    TypeId = H5T.copy(H5T.H5Type.IEEE_F64LE);
                    break;
                default:
                    throw new NotImplementedException();
            }

            M = m;
            N = n;
            long[] dims = { M, N };
            SpaceId = H5S.create_simple(2, dims);
            DataSetId = H5D.create(FileId, "/" + varname,
                                               TypeId, SpaceId);

            H5DataTypeId attributeTypeId = H5T.create(H5T.CreateClass.STRING, matlabClass.Length);
            H5DataSpaceId attributeSpaceId = H5S.create(H5S.H5SClass.SCALAR);
            H5AttributeId attributeId = H5A.createByName(GroupId, varname, "MATLAB_class", attributeTypeId, attributeSpaceId);
            byte[] asciiBytes = Encoding.ASCII.GetBytes(matlabClass);
            H5A.write(attributeId, attributeTypeId, new H5Array<byte>(asciiBytes));
            H5A.close(attributeId);
            H5S.close(attributeSpaceId);
            H5T.close(attributeTypeId);
            RowCursor = 0;
            ColCursor = 0;
        }

        private void PrependMatlabHeader(string filename)
        {
            byte[] header = new byte[512];

            string headerText = "MATLAB 7.3 MAT-file";
            byte[] headerTextBytes = Encoding.ASCII.GetBytes(headerText);

            for (int i = 0; i < headerText.Length; i++) header[i] = headerTextBytes[i];

            header[124] = 0;
            header[125] = 2;
            header[126] = Encoding.ASCII.GetBytes("I")[0];
            header[127] = Encoding.ASCII.GetBytes("M")[0];

            string tempfile = Path.GetTempFileName();

            using (var newFile = new FileStream(tempfile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                newFile.Write(header, 0, header.Length);

                using (var oldFile = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    oldFile.CopyTo(newFile);
                }
            }
            File.Copy(tempfile, filename, true);
        }

        public void WriteColumn(int[] data)
        {
            long[] start = { 0, ColCursor };
            long[] count = { M, 1 };
            H5S.selectHyperslab(SpaceId, H5S.SelectOperator.SET, start, count);
            long[] memDims = { M, 1 };
            H5DataSpaceId memSpaceId = H5S.create_simple(2, memDims);
            H5PropertyListId propListId = H5P.create(H5P.PropertyListClass.DATASET_XFER);
            H5D.write(DataSetId, TypeId, memSpaceId, SpaceId, propListId, new H5Array<int>(data));
            ColCursor++;
            RowCursor = 0;
        }

        public void WriteColumn(double[] data)
        {
            long[] start = { 0, ColCursor };
            long[] count = { M, 1 };
            H5S.selectHyperslab(SpaceId, H5S.SelectOperator.SET, start, count);
            long[] memDims = { M, 1 };
            H5DataSpaceId memSpaceId = H5S.create_simple(2, memDims);
            H5PropertyListId propListId = H5P.create(H5P.PropertyListClass.DATASET_XFER);
            H5D.write(DataSetId, TypeId, memSpaceId, SpaceId, propListId, new H5Array<double>(data));
            ColCursor++;
            RowCursor = 0;
        }

        public void WriteRow(int[] data)
        {
            long[] start = { RowCursor, 0 };
            long[] count = { 1, N };

            H5S.selectHyperslab(SpaceId, H5S.SelectOperator.SET, start, count);

            //RegionReference selection = H5R.createRegionReference(groupId, "R1", spaceId);
            //H5DataSpaceId selectionSpaceId = H5R.getRegion(groupId, selection);

            long[] memDims = { 1, N };
            H5DataSpaceId memSpaceId = H5S.create_simple(2, memDims);

            H5PropertyListId propListId = H5P.create(H5P.PropertyListClass.DATASET_XFER);

            // Writes along the rows of the selection
            H5D.write(DataSetId, TypeId, memSpaceId, SpaceId, propListId, new H5Array<int>(data));
            H5S.close(memSpaceId);
            RowCursor++;
            ColCursor = 0;
        }

        public void WriteRow(double[] data)
        {
            long[] start = { RowCursor, 0 };
            long[] count = { 1, N };

            H5S.selectHyperslab(SpaceId, H5S.SelectOperator.SET, start, count);

            //RegionReference selection = H5R.createRegionReference(groupId, "R1", spaceId);
            //H5DataSpaceId selectionSpaceId = H5R.getRegion(groupId, selection);

            long[] memDims = { 1, N };
            H5DataSpaceId memSpaceId = H5S.create_simple(2, memDims);

            H5PropertyListId propListId = H5P.create(H5P.PropertyListClass.DATASET_XFER);

            // Writes along the rows of the selection
            H5D.write(DataSetId, TypeId, memSpaceId, SpaceId, propListId, new H5Array<double>(data));
            H5S.close(memSpaceId);
            RowCursor++;
            ColCursor = 0;
        }

        public void Close()
        {
            H5D.close(DataSetId);
            H5S.close(SpaceId);
            H5T.close(TypeId);
            H5G.close(GroupId);
            H5F.close(FileId);
            PrependMatlabHeader(_FileName);
        }

        public void Dispose()
        {
            Close();
        }
    }
}
