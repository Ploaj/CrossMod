﻿
namespace SSBHLib.Formats
{
    [SSBHFileAttribute("HSEM")]
    public class MESH : ISSBH_File
    {
        public uint Magic { get; set; }
        
        public ushort VersionMajor { get; set; } // 0x0001
        
        public ushort VersionMinor { get; set; } // 0x000A
        
        public long HeaderSize { get; set; }
        
        public float[] HeaderFloats { get; set; } = new float[26];

        public MESH_Object[] Objects { get; set; }

        public int[] BufferSizes { get; set; }

        public long UnknownSize { get; set; }

        public MESH_Buffer[] VertexBuffers { get; set; }

        public byte[] PolygonBuffer { get; set; }

        public MESH_RiggingGroup[] RiggingBuffers { get; set; }
    }

    public class MESH_RiggingGroup : ISSBH_File
    {
        public string Name { get; set; }
        
        public long SubMeshIndex { get; set; }
    
        public long Unk_Flags { get; set; }

        public MESH_BoneBuffer[] Buffers { get; set; }
    }

    public class MESH_BoneBuffer : ISSBH_File
    {
        public string BoneName { get; set; }

        public byte[] Data { get; set; }
    }

    public class MESH_Buffer : ISSBH_File
    {
        public byte[] Buffer { get; set; }
    }

    public class MESH_Object : ISSBH_File
    {
        public string Name { get; set; }
        
        public long Unk { get; set; }
        
        public string ParentBoneName { get; set; }
        
        public int VertexCount { get; set; }
        
        public int PolygonCount { get; set; }
        
        public uint Unk2 { get; set; }
        
        public int VertexIndexOffset { get; set; }
        
        public int VertexIndexOffset2 { get; set; }
        
        public int Unk4 { get; set; }
        
        public int BID { get; set; }
        
        public int Stride { get; set; }
        
        public int Stride2 { get; set; }
        
        public int Unk6 { get; set; }
        
        public int Unk7 { get; set; }
        
        public uint PolygonIndexOffset { get; set; }
        
        public int Unk8 { get; set; }
    
        public int Unk9 { get; set; }
        
        public int Unk10 { get; set; }
        
        public int Unk11 { get; set; }
        
        public float[] Floats { get; set; } = new float[26];

        public MESH_Attribute[] Attributes { get; set; }
    }

    public class MESH_Attribute : ISSBH_File
    {
        public int Index { get; set; }
        
        public int DataType { get; set; }
        
        public int BufferIndex { get; set; }
        
        public int BufferOffset { get; set; }
        
        public int Unk4_0 { get; set; }
        
        public int Unk5_0 { get; set; }

        public string Name { get; set; }

        public MESH_AttributeString[] AttributeStrings { get; set; }
    }

    public class MESH_AttributeString : ISSBH_File
    {
        public string Name { get; set; }
    }
}