using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HDF5DotNet;

namespace lasercom.io
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

        private H5FileId _FileId;
        public H5FileId FileId
        {
            get
            {
                return _FileId;
            }
            private set
            {
                _FileId = value;
            }
        }

        private H5GroupId _GroupId;
        public H5GroupId GroupId
        {
            get
            {
                return _GroupId;
            }
            private set
            {
                _GroupId = value;
            }
        }

        Dictionary<string, MatVar> Variables;

        public MatVar this[string Name]
        {
            get
            {
                MatVar V;
                Variables.TryGetValue(Name, out V);
                return V;
            }
        }

        private bool _Disposed = false;
        public bool Disposed
        {
            get
            {
                return _Disposed;
            }
            private set
            {
                _Disposed = value;
            }
        }

        private bool _Closed = false;
        public bool Closed
        {
            get
            {
                return _Closed;
            }
            private set
            {
                _Closed = value;
            }
        }

        private bool PrependOnClose = true;

        public MatFile(string fileName)
        {
            _FileName = fileName;
            FileId = H5F.create(FileName, H5F.CreateMode.ACC_TRUNC);
            GroupId = H5G.open(FileId, "/");

            Variables = new Dictionary<string, MatVar>();
        }

        public MatVar<T> CreateVariable<T>(string Name, params long[] Dims)
        {
            MatVar<T> V = new MatVar<T>(Name, GroupId, Dims);
            Variables.Add(Name, V);
            return V;
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

            PrependOnClose = false;
        }

        public void Close()
        {
            if (!Closed)
            {
                foreach (var V in Variables.Values) V.Close();

                H5G.close(GroupId);
                H5F.close(FileId);

                if (PrependOnClose)
                    PrependMatlabHeader(_FileName);

                Closed = true;
            }
        }

        public void Reopen()
        {
            if (Disposed) throw new ObjectDisposedException(FileName + " disposed");
            if (Closed)
            {
                FileId = H5F.open(FileName, H5F.OpenMode.ACC_RDWR);
                GroupId = H5G.open(FileId, "/");
                foreach (var V in Variables.Values)
                {
                    V.Open();
                }
                Closed = false;
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
