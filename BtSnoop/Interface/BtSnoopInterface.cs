using System;
using System.Collections.Generic;
using System.Linq;

namespace BtSnoop.Interface
{
    public class BtSnoopInterface
    {
        private int _offset;
        
        public BtSnoopInterface()
        {
            Adb.ResetFile();   
        }
        
        public IEnumerable<byte> RetrieveDelta()
        {
            var deltaContent = Adb.FileContent(_offset);
            deltaContent = deltaContent.Length == 0 ? deltaContent : deltaContent.Substring(1);

            var hexValueArray = deltaContent.Split(" ");
            if (hexValueArray.Length == 0 || hexValueArray[0] == "") return new byte[] {};

            var byteArray = hexValueArray.Select(hex => Convert.ToByte(hex, 16)).ToArray();
            _offset += byteArray.Length;

            return byteArray;
        }
    }
}