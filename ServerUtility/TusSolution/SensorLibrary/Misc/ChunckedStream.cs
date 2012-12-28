using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SensorLibrary
{
    public class ChunckedStreamController
        : IDisposable
    {
        public int ChunckSize { get; set; }
        public Stream BaseStream { get; private set; }
        public byte EmptyData { get; set; }

        private Queue<byte> readRemains = new Queue<byte>();
        private Queue<byte> writeRemains = new Queue<byte>();

        public ChunckedStreamController(Stream st, int chunk)
        {
            this.ChunckSize = chunk;
            this.BaseStream = st;
        }

        public int ReadChunck(byte[] buffer, int offset)
        {
            return this.BaseStream.Read(buffer, offset, this.ChunckSize);
        }

        public void WriteChunck(byte[] buffer, int offset)
        {
            this.BaseStream.Write(buffer, offset, this.ChunckSize);
        }

        public byte ReadByte()
        {
            var buf = new byte[1];
            var res = this.Read(buf, 0, 1);
            if (res != 1)
                throw new InvalidOperationException("無効な戻り値 : Read() != 1");
            return buf[0];
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int pos = 0;
            do
            {
                if (count <= pos)
                    break;
                if (readRemains.Count() == 0)
                {
                    var prebuf = new byte[this.ChunckSize];
                    this.ReadChunck(prebuf, 0);
                    this.readRemains = new Queue<byte>(prebuf);
                }
                buffer[pos + offset] = readRemains.Dequeue();
                pos++;
            } while (true);

            return pos;
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            int pos = 0;

            do
            {
                if (count <= pos)
                    break;
                if (writeRemains.Count() == this.ChunckSize)
                    this.Flush();

                this.writeRemains.Enqueue(buffer[offset + pos]);
                pos++;
            } while (true);
        }

        public void Flush()
        {
            while (writeRemains.Count() < this.ChunckSize)
            {
                this.writeRemains.Enqueue(this.EmptyData);
            }
            var sendbuf = this.writeRemains.ToArray();
            this.WriteChunck(sendbuf, 0);
            this.writeRemains.Clear();

        }

        public void Close()
        {
            this.Dispose();
        }

        #region Dispose-Finalize Pattern
        private bool __disposed = false;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (__disposed) return;
            if (disposing)
            {

            }

            if (BaseStream != null)
            {
                try
                {
                    BaseStream.Close();
                }
                catch (IOException)
                { }
            }

            //base.Dispose();
            __disposed = true;
        }

        ~ChunckedStreamController()
        {
            this.Dispose(false);
        }
        #endregion

    }
}