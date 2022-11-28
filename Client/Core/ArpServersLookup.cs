using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using static Common.Import;

namespace Client.Core
{
    internal class ArpServersLookup : IServersLookup
    {
        public IEnumerable<string> GetServers()
        {
            IntPtr buff;
            int requiredLen = 0;
            var ret = new List<string>();
            long result = GetIpNetTable(IntPtr.Zero, ref requiredLen, false);

            try
            {
                buff = Marshal.AllocCoTaskMem(requiredLen);
                result = GetIpNetTable(buff, ref requiredLen, false);

                int entries = Marshal.ReadInt32(buff);
                IntPtr entryBuffer = new IntPtr(buff.ToInt64() + Marshal.SizeOf(typeof(int)));

                MIB_IPNETROW[] arpTable = new MIB_IPNETROW[entries];

                for (int i = 0; i < entries; i++)
                {
                    int currentIndex = i * Marshal.SizeOf(typeof(MIB_IPNETROW));
                    IntPtr newStruct = new IntPtr(entryBuffer.ToInt64() + currentIndex);
                    arpTable[i] = (MIB_IPNETROW)Marshal.PtrToStructure(newStruct, typeof(MIB_IPNETROW));
                }

                for (int i = 0; i < entries; i++)
                {
                    MIB_IPNETROW entry = arpTable[i];
                    var type = entry.dwType;
                    if (type != 3)
                        continue;

                    var addr = new IPAddress(BitConverter.GetBytes(entry.dwAddr));
                    try
                    {
                        //var hostName = Dns.GetHostByAddress(addr);
                        ret.Add(addr.ToString());
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch
            {
                return Enumerable.Empty<string>();
            }

            return ret;
        }
    }
}
