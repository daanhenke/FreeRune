using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FreeRune.Server.Common
{
    public static class BinaryUtilities
    {
        public static byte[] StructureToByteArray<StructType>(StructType structure)
        {
            int size = Marshal.SizeOf<StructType>();
            byte[] array = new byte[size];

            IntPtr pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, pointer, true);
            Marshal.Copy(pointer, array, 0, size);
            Marshal.FreeHGlobal(pointer);

            return array;
        }

        public static StructType ByteArrayToStructure<StructType>(byte[] array)
        {
            StructType structure;

            int size = Marshal.SizeOf<StructType>();
            IntPtr pointer = Marshal.AllocHGlobal(size);

            Marshal.Copy(array, 0, pointer, size);

            structure = Marshal.PtrToStructure<StructType>(pointer);
            Marshal.FreeHGlobal(pointer);

            return structure;
        }

        public static void WriteStructure<StructType>(BinaryWriter writer, StructType structure)
        {
            byte[] bytes = StructureToByteArray<StructType>(structure);
            writer.Write(bytes);
        }

        public static StructType ReadStructure<StructType>(BinaryReader reader)
        {
            int size = Marshal.SizeOf<StructType>();
            return ByteArrayToStructure<StructType>(reader.ReadBytes(size));
        }

        public static string ReadRSString(BinaryReader reader)
        {
            StringBuilder userNameBuilder = new StringBuilder();
            while (true)
            {
                char currentCharacter = reader.ReadChar();
                if (currentCharacter == '\n')
                {
                    return userNameBuilder.ToString();
                }
                else
                {
                    userNameBuilder.Append(currentCharacter);
                }
            }
        }
    }
}