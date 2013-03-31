using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive;

namespace Tus.Communication
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct DeviceID
        : IEquatable<DeviceID>
    {
        public DeviceID(ushort parent, byte module)
        {
            this.ParentPart = parent;
            this.ModulePart = 0;
            this.ModuleAddr = module;
        }

        public DeviceID(ushort parent, byte module, byte inter)
        {
            this.ParentPart = parent;
            this.ModulePart = 0;
            this.ModuleAddr = module;
            this.InternalAddr = inter;
        }

        public ushort ParentPart;
        //subnetaddr : 16
        public ushort ModulePart;
        //moduleaddr : 8
        //internaladdr : 8

        public byte ModuleAddr
        {
            get
            {
                return (byte)(this.ModulePart >> 8);
            }
            set
            {
                this.ModulePart &= 0x00ff;
                this.ModulePart |= (ushort)(value << 8);
            }
        }

        public byte InternalAddr
        {
            get
            {
                return (byte)(this.ModulePart & 0x00ff);
            }
            set
            {
                this.ModulePart &= 0xff00;
                this.ModulePart |= (ushort)(value);
            }
        }

        public bool IsMatched(int parent, int module, int inter)
        {
            return (this.ParentPart == parent || parent < 0)
                   && (this.ModuleAddr == module || module < 0)
                   && (this.InternalAddr == inter || inter < 0);
        }

        public int GetUniqueIdByBoard()
        {
            return this.ParentPart << 8 | this.ModuleAddr;
        }


        #region implementation of IEqualable
        public static bool operator ==(DeviceID A, DeviceID B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A.ParentPart == B.ParentPart && A.ModulePart == B.ModulePart);
        }
        public static bool operator !=(DeviceID A, DeviceID B) { return !(A == B); }

        public bool Equals(DeviceID other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this.ModulePart.GetHashCode() ^ this.ParentPart.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this == (DeviceID)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.ParentPart, this.ModuleAddr, this.InternalAddr);
        }

    }
}
