﻿namespace SSBHLib.IO
{
    /// <summary>
    /// Generated code for parsing SSBH types using a <see cref="SsbhParser"/>.
    /// Changes made to this file will be overridden.    
    /// </summary>
    public static class Parsers
    {
        public static Formats.Hlpb ParseHlpb(this SsbhParser parser)
        {
            var result = new Formats.Hlpb();
            result.Magic = parser.ReadUInt32();      
            result.VersionMajor = parser.ReadUInt16();      
            result.VersionMinor = parser.ReadUInt16();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.AimEntries = new Formats.HlpbRotateAim[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.AimEntries[i] = parser.ParseHlpbRotateAim();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.InterpolationEntries = new Formats.HlpbRotateInterpolation[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.InterpolationEntries[i] = parser.ParseHlpbRotateInterpolation();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.List1 = new System.Int32[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.List1[i] = parser.ReadInt32();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.List2 = new System.Int32[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.List2[i] = parser.ReadInt32();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.HlpbRotateAim ParseHlpbRotateAim(this SsbhParser parser)
        {
            var result = new Formats.HlpbRotateAim();
            result.Name = parser.ReadOffsetReadString();
            result.AimBoneName1 = parser.ReadOffsetReadString();
            result.AimBoneName2 = parser.ReadOffsetReadString();
            result.AimType1 = parser.ReadOffsetReadString();
            result.AimType2 = parser.ReadOffsetReadString();
            result.TargetBoneName1 = parser.ReadOffsetReadString();
            result.TargetBoneName2 = parser.ReadOffsetReadString();
            result.Unknown1 = parser.ReadInt32();      
            result.Unknown2 = parser.ReadInt32();      
            result.Unknown3 = parser.ReadSingle();      
            result.Unknown4 = parser.ReadSingle();      
            result.Unknown5 = parser.ReadSingle();      
            result.Unknown6 = parser.ReadSingle();      
            result.Unknown7 = parser.ReadSingle();      
            result.Unknown8 = parser.ReadSingle();      
            result.Unknown9 = parser.ReadSingle();      
            result.Unknown10 = parser.ReadSingle();      
            result.Unknown11 = parser.ReadSingle();      
            result.Unknown12 = parser.ReadSingle();      
            result.Unknown13 = parser.ReadSingle();      
            result.Unknown14 = parser.ReadSingle();      
            result.Unknown15 = parser.ReadSingle();      
            result.Unknown16 = parser.ReadSingle();      
            result.Unknown17 = parser.ReadSingle();      
            result.Unknown18 = parser.ReadSingle();      
            result.Unknown19 = parser.ReadSingle();      
            result.Unknown20 = parser.ReadSingle();      
            result.Unknown21 = parser.ReadSingle();      
            result.Unknown22 = parser.ReadSingle();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.HlpbRotateInterpolation ParseHlpbRotateInterpolation(this SsbhParser parser)
        {
            var result = new Formats.HlpbRotateInterpolation();
            result.Name = parser.ReadOffsetReadString();
            result.BoneName = parser.ReadOffsetReadString();
            result.RootBoneName = parser.ReadOffsetReadString();
            result.ParentBoneName = parser.ReadOffsetReadString();
            result.DriverBoneName = parser.ReadOffsetReadString();
            result.Type = parser.ReadUInt32();      
            result.AoIx = parser.ReadSingle();      
            result.AoIy = parser.ReadSingle();      
            result.AoIz = parser.ReadSingle();      
            result.Quat1X = parser.ReadSingle();      
            result.Quat1Y = parser.ReadSingle();      
            result.Quat1Z = parser.ReadSingle();      
            result.Quat1W = parser.ReadSingle();      
            result.Quat2X = parser.ReadSingle();      
            result.Quat2Y = parser.ReadSingle();      
            result.Quat2Z = parser.ReadSingle();      
            result.Quat2W = parser.ReadSingle();      
            result.MinRangeX = parser.ReadSingle();      
            result.MinRangeY = parser.ReadSingle();      
            result.MinRangeZ = parser.ReadSingle();      
            result.MaxRangeX = parser.ReadSingle();      
            result.MaxRangeY = parser.ReadSingle();      
            result.MaxRangeZ = parser.ReadSingle();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Skel ParseSkel(this SsbhParser parser)
        {
            var result = new Formats.Skel();
            result.Magic = parser.ReadUInt32();      
            result.MajorVersion = parser.ReadUInt16();      
            result.MinorVersion = parser.ReadUInt16();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.BoneEntries = new Formats.SkelBoneEntry[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.BoneEntries[i] = parser.ParseSkelBoneEntry();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.WorldTransform = new Formats.SkelMatrix[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.WorldTransform[i] = parser.ParseSkelMatrix();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.InvWorldTransform = new Formats.SkelMatrix[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.InvWorldTransform[i] = parser.ParseSkelMatrix();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Transform = new Formats.SkelMatrix[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Transform[i] = parser.ParseSkelMatrix();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.InvTransform = new Formats.SkelMatrix[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.InvTransform[i] = parser.ParseSkelMatrix();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.SkelBoneEntry ParseSkelBoneEntry(this SsbhParser parser)
        {
            var result = new Formats.SkelBoneEntry();
            result.Name = parser.ReadOffsetReadString();
            result.Id = parser.ReadInt16();      
            result.ParentId = parser.ReadInt16();      
            result.Type = parser.ReadInt32();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.SkelMatrix ParseSkelMatrix(this SsbhParser parser)
        {
            var result = new Formats.SkelMatrix();
            result.M11 = parser.ReadSingle();      
            result.M12 = parser.ReadSingle();      
            result.M13 = parser.ReadSingle();      
            result.M14 = parser.ReadSingle();      
            result.M21 = parser.ReadSingle();      
            result.M22 = parser.ReadSingle();      
            result.M23 = parser.ReadSingle();      
            result.M24 = parser.ReadSingle();      
            result.M31 = parser.ReadSingle();      
            result.M32 = parser.ReadSingle();      
            result.M33 = parser.ReadSingle();      
            result.M34 = parser.ReadSingle();      
            result.M41 = parser.ReadSingle();      
            result.M42 = parser.ReadSingle();      
            result.M43 = parser.ReadSingle();      
            result.M44 = parser.ReadSingle();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Modl ParseModl(this SsbhParser parser)
        {
            var result = new Formats.Modl();
            result.Magic = parser.ReadUInt32();      
            result.MajorVersion = parser.ReadUInt16();      
            result.MinorVersion = parser.ReadUInt16();      
            result.ModelFileName = parser.ReadOffsetReadString();
            result.SkeletonFileName = parser.ReadOffsetReadString();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.MaterialFileNames = new Formats.ModlMaterialName[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.MaterialFileNames[i] = parser.ParseModlMaterialName();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            result.UnknownFileName = parser.ReadOffsetReadString();
            result.MeshString = parser.ReadOffsetReadString();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.ModelEntries = new Formats.ModlEntry[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.ModelEntries[i] = parser.ParseModlEntry();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.ModlMaterialName ParseModlMaterialName(this SsbhParser parser)
        {
            var result = new Formats.ModlMaterialName();
            result.MaterialFileName = parser.ReadOffsetReadString();

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.ModlEntry ParseModlEntry(this SsbhParser parser)
        {
            var result = new Formats.ModlEntry();
            result.MeshName = parser.ReadOffsetReadString();
            result.SubIndex = parser.ReadInt64();      
            result.MaterialName = parser.ReadOffsetReadString();

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.Nrpd ParseNrpd(this SsbhParser parser)
        {
            var result = new Formats.Rendering.Nrpd();
            result.Magic = parser.ReadUInt32();      
            result.MajorVersion = parser.ReadUInt16();      
            result.MinorVersion = parser.ReadUInt16();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.FrameBufferContainers = new Formats.Rendering.NrpdFrameBufferContainer[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.FrameBufferContainers[i] = parser.ParseNrpdFrameBufferContainer();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.StateContainers = new Formats.Rendering.NrpdStateContainer[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.StateContainers[i] = parser.ParseNrpdStateContainer();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.RenderPasses = new Formats.Rendering.NrpdRenderPass[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.RenderPasses[i] = parser.ParseNrpdRenderPass();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.NrpdFrameBuffer ParseNrpdFrameBuffer(this SsbhParser parser)
        {
            var result = new Formats.Rendering.NrpdFrameBuffer();
            result.Name = parser.ReadOffsetReadString();
            result.Width = parser.ReadUInt32();      
            result.Height = parser.ReadUInt32();      
            result.Unk1 = parser.ReadUInt64();      
            result.Unk2 = parser.ReadUInt32();      
            result.Unk3 = parser.ReadUInt32();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.NrpdFrameBufferContainer ParseNrpdFrameBufferContainer(this SsbhParser parser)
        {
            var result = new Formats.Rendering.NrpdFrameBufferContainer();
            result.FrameBuffer = parser.ParseNrpdFrameBuffer();      
            result.Type = parser.ReadUInt64();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.NrpdRenderPass ParseNrpdRenderPass(this SsbhParser parser)
        {
            var result = new Formats.Rendering.NrpdRenderPass();
            result.Name = parser.ReadOffsetReadString();
            result.Offset2 = parser.ReadUInt64();      
            result.Type2 = parser.ReadUInt64();      
            result.Offset3 = parser.ReadUInt64();      
            result.Type3 = parser.ReadUInt64();      
            result.UnkString = parser.ReadOffsetReadString();
            result.Type4 = parser.ReadUInt64();      
            result.Padding = parser.ReadUInt64();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.NrpdSampler ParseNrpdSampler(this SsbhParser parser)
        {
            var result = new Formats.Rendering.NrpdSampler();
            result.Name = parser.ReadOffsetReadString();
            result.WrapS = parser.ReadInt32();      
            result.WrapT = parser.ReadInt32();      
            result.WrapR = parser.ReadInt32();      
            result.Unk4 = parser.ReadInt32();      
            result.Unk5 = parser.ReadInt32();      
            result.Unk6 = parser.ReadInt32();      
            result.Unk7 = parser.ReadInt32();      
            result.Unk8 = parser.ReadInt32();      
            result.Unk9 = parser.ReadInt32();      
            result.Unk10 = parser.ReadInt32();      
            result.Unk11 = parser.ReadInt32();      
            result.Unk12 = parser.ReadInt32();      
            result.Unk13 = parser.ReadSingle();      
            result.Unk14 = parser.ReadInt32();      
            result.Unk15 = parser.ReadInt32();      
            result.Unk16 = parser.ReadInt32();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.NrpdStateContainer ParseNrpdStateContainer(this SsbhParser parser)
        {
            var result = new Formats.Rendering.NrpdStateContainer();
            result.StateObject = parser.ParseNrpdSampler();      
            result.Type = parser.ReadUInt64();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.Shdr ParseShdr(this SsbhParser parser)
        {
            var result = new Formats.Rendering.Shdr();
            result.Magic = parser.ReadUInt32();      
            result.MajorVersion = parser.ReadUInt16();      
            result.MinorVersion = parser.ReadUInt16();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Shaders = new Formats.Rendering.ShdrShader[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Shaders[i] = parser.ParseShdrShader();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Rendering.ShdrShader ParseShdrShader(this SsbhParser parser)
        {
            var result = new Formats.Rendering.ShdrShader();
            result.Name = parser.ReadOffsetReadString();
            result.Unk1 = parser.ReadUInt32();      
            result.Unk2 = parser.ReadUInt32();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.ShaderBinary = new System.Byte[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.ShaderBinary[i] = parser.ReadByte();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            result.ShaderFileSize = parser.ReadInt64();      
            result.Padding1 = parser.ReadInt64();      
            result.Padding2 = parser.ReadInt64();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Meshes.Mesh ParseMesh(this SsbhParser parser)
        {
            var result = new Formats.Meshes.Mesh();
            result.Magic = parser.ReadUInt32();      
            result.VersionMajor = parser.ReadUInt16();      
            result.VersionMinor = parser.ReadUInt16();      
            result.ModelName = parser.ReadOffsetReadString();
            result.BoundingSphereX = parser.ReadSingle();      
            result.BoundingSphereY = parser.ReadSingle();      
            result.BoundingSphereZ = parser.ReadSingle();      
            result.BoundingSphereRadius = parser.ReadSingle();      
            result.MinBoundingBoxX = parser.ReadSingle();      
            result.MinBoundingBoxY = parser.ReadSingle();      
            result.MinBoundingBoxZ = parser.ReadSingle();      
            result.MaxBoundingBoxX = parser.ReadSingle();      
            result.MaxBoundingBoxY = parser.ReadSingle();      
            result.MaxBoundingBoxZ = parser.ReadSingle();      
            result.ObbCenterX = parser.ReadSingle();      
            result.ObbCenterY = parser.ReadSingle();      
            result.ObbCenterZ = parser.ReadSingle();      
            result.M11 = parser.ReadSingle();      
            result.M12 = parser.ReadSingle();      
            result.M13 = parser.ReadSingle();      
            result.M21 = parser.ReadSingle();      
            result.M22 = parser.ReadSingle();      
            result.M23 = parser.ReadSingle();      
            result.M31 = parser.ReadSingle();      
            result.M32 = parser.ReadSingle();      
            result.M33 = parser.ReadSingle();      
            result.ObbSizeX = parser.ReadSingle();      
            result.ObbSizeY = parser.ReadSingle();      
            result.ObbSizeZ = parser.ReadSingle();      
            result.UnkBounding0 = parser.ReadSingle();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Objects = new Formats.Meshes.MeshObject[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Objects[i] = parser.ParseMeshObject();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.BufferSizes = new System.Int32[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.BufferSizes[i] = parser.ReadInt32();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            result.PolygonIndexSize = parser.ReadInt64();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.VertexBuffers = new Formats.Meshes.MeshBuffer[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.VertexBuffers[i] = parser.ParseMeshBuffer();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.PolygonBuffer = new System.Byte[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.PolygonBuffer[i] = parser.ReadByte();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.RiggingBuffers = new Formats.Meshes.MeshRiggingGroup[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.RiggingBuffers[i] = parser.ParseMeshRiggingGroup();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            result.UnknownOffset = parser.ReadInt64();      
            result.UnknownSize = parser.ReadInt64();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Meshes.MeshAttribute ParseMeshAttribute(this SsbhParser parser)
        {
            var result = new Formats.Meshes.MeshAttribute();
            result.Index = parser.ReadInt32();      
            result.DataType = parser.ReadInt32();      
            result.BufferIndex = parser.ReadInt32();      
            result.BufferOffset = parser.ReadInt32();      
            result.Unk4 = parser.ReadInt32();      
            result.Unk5 = parser.ReadInt32();      
            result.Name = parser.ReadOffsetReadString();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.AttributeStrings = new Formats.Meshes.MeshAttributeString[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.AttributeStrings[i] = parser.ParseMeshAttributeString();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Meshes.MeshAttributeString ParseMeshAttributeString(this SsbhParser parser)
        {
            var result = new Formats.Meshes.MeshAttributeString();
            result.Name = parser.ReadOffsetReadString();

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Meshes.MeshBoneBuffer ParseMeshBoneBuffer(this SsbhParser parser)
        {
            var result = new Formats.Meshes.MeshBoneBuffer();
            result.BoneName = parser.ReadOffsetReadString();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Data = new System.Byte[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Data[i] = parser.ReadByte();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Meshes.MeshBuffer ParseMeshBuffer(this SsbhParser parser)
        {
            var result = new Formats.Meshes.MeshBuffer();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Buffer = new System.Byte[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Buffer[i] = parser.ReadByte();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Meshes.MeshObject ParseMeshObject(this SsbhParser parser)
        {
            var result = new Formats.Meshes.MeshObject();
            result.Name = parser.ReadOffsetReadString();
            result.SubMeshIndex = parser.ReadInt64();      
            result.ParentBoneName = parser.ReadOffsetReadString();
            result.VertexCount = parser.ReadInt32();      
            result.IndexCount = parser.ReadInt32();      
            result.Unk2 = parser.ReadUInt32();      
            result.VertexOffset = parser.ReadInt32();      
            result.VertexOffset2 = parser.ReadInt32();      
            result.FinalBufferOffset = parser.ReadInt32();      
            result.BufferIndex = parser.ReadInt32();      
            result.Stride = parser.ReadInt32();      
            result.Stride2 = parser.ReadInt32();      
            result.Unk6 = parser.ReadInt32();      
            result.Unk7 = parser.ReadInt32();      
            result.ElementOffset = parser.ReadUInt32();      
            result.Unk8 = parser.ReadInt32();      
            result.DrawElementType = parser.ReadInt32();      
            result.HasRigging = parser.ReadInt32();      
            result.Unk11 = parser.ReadInt32();      
            result.UnkBounding0 = parser.ReadSingle();      
            result.BoundingSphereX = parser.ReadSingle();      
            result.BoundingSphereY = parser.ReadSingle();      
            result.BoundingSphereZ = parser.ReadSingle();      
            result.BoundingSphereRadius = parser.ReadSingle();      
            result.MinBoundingBoxX = parser.ReadSingle();      
            result.MinBoundingBoxY = parser.ReadSingle();      
            result.MinBoundingBoxZ = parser.ReadSingle();      
            result.MaxBoundingBoxX = parser.ReadSingle();      
            result.MaxBoundingBoxY = parser.ReadSingle();      
            result.MaxBoundingBoxZ = parser.ReadSingle();      
            result.ObbCenterX = parser.ReadSingle();      
            result.ObbCenterY = parser.ReadSingle();      
            result.ObbCenterZ = parser.ReadSingle();      
            result.M11 = parser.ReadSingle();      
            result.M12 = parser.ReadSingle();      
            result.M13 = parser.ReadSingle();      
            result.M21 = parser.ReadSingle();      
            result.M22 = parser.ReadSingle();      
            result.M23 = parser.ReadSingle();      
            result.M31 = parser.ReadSingle();      
            result.M32 = parser.ReadSingle();      
            result.M33 = parser.ReadSingle();      
            result.ObbSizeX = parser.ReadSingle();      
            result.ObbSizeY = parser.ReadSingle();      
            result.ObbSizeZ = parser.ReadSingle();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Attributes = new Formats.Meshes.MeshAttribute[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Attributes[i] = parser.ParseMeshAttribute();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Meshes.MeshRiggingGroup ParseMeshRiggingGroup(this SsbhParser parser)
        {
            var result = new Formats.Meshes.MeshRiggingGroup();
            result.Name = parser.ReadOffsetReadString();
            result.SubMeshIndex = parser.ReadInt64();      
            result.Flags = parser.ReadInt64();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Buffers = new Formats.Meshes.MeshBoneBuffer[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Buffers[i] = parser.ParseMeshBoneBuffer();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlAttribute ParseMatlAttribute(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlAttribute();
            result.ParamId = (Formats.Materials.MatlEnums.ParamId)parser.ReadUInt64();      
            result.OffsetToData = parser.Position + parser.ReadInt64();      
            result.DataType = (Formats.Materials.MatlEnums.ParamDataType)parser.ReadUInt64();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlEntry ParseMatlEntry(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlEntry();
            result.MaterialLabel = parser.ReadOffsetReadString();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Attributes = new Formats.Materials.MatlAttribute[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Attributes[i] = parser.ParseMatlAttribute();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            result.MaterialName = parser.ReadOffsetReadString();

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.Matl ParseMatl(this SsbhParser parser)
        {
            var result = new Formats.Materials.Matl();
            result.Magic = parser.ReadUInt32();      
            result.MajorVersion = parser.ReadInt16();      
            result.MinorVersion = parser.ReadInt16();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Entries = new Formats.Materials.MatlEntry[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Entries[i] = parser.ParseMatlEntry();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Animation.Anim ParseAnim(this SsbhParser parser)
        {
            var result = new Formats.Animation.Anim();
            result.Magic = parser.ReadUInt32();      
            result.VersionMajor = parser.ReadUInt16();      
            result.VersionMinor = parser.ReadUInt16();      
            result.FrameCount = parser.ReadSingle();      
            result.Unk1 = parser.ReadUInt16();      
            result.Unk2 = parser.ReadUInt16();      
            result.Name = parser.ReadOffsetReadString();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Animations = new Formats.Animation.AnimGroup[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Animations[i] = parser.ParseAnimGroup();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Buffer = new System.Byte[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Buffer[i] = parser.ReadByte();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Animation.AnimGroup ParseAnimGroup(this SsbhParser parser)
        {
            var result = new Formats.Animation.AnimGroup();
            result.Type = (Formats.Animation.AnimType)parser.ReadUInt64();      
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Nodes = new Formats.Animation.AnimNode[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Nodes[i] = parser.ParseAnimNode();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Animation.AnimNode ParseAnimNode(this SsbhParser parser)
        {
            var result = new Formats.Animation.AnimNode();
            result.Name = parser.ReadOffsetReadString();
            {
                // TODO: Extract this code to a method?
                bool inline = false; // TODO: Add proper check for this.
                long absoluteOffset = parser.GetOffset(inline);
                long elementCount = parser.ReadInt64(); // TODO: Depends on inline
                long previousPosition = parser.Position;
                parser.Seek(absoluteOffset);

                // TODO: Optimize reading arrays of blittable types.
                result.Tracks = new Formats.Animation.AnimTrack[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    result.Tracks[i] = parser.ParseAnimTrack();
                }

                if (!inline)
                    parser.Seek(previousPosition); 
            }

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Animation.AnimTrack ParseAnimTrack(this SsbhParser parser)
        {
            var result = new Formats.Animation.AnimTrack();
            result.Name = parser.ReadOffsetReadString();
            result.Flags = parser.ReadUInt32();      
            result.FrameCount = parser.ReadUInt32();      
            result.Unk3 = parser.ReadUInt32();      
            result.DataOffset = parser.ReadUInt32();      
            result.DataSize = parser.ReadInt64();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlAttribute.MatlBlendState ParseMatlBlendState(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlAttribute.MatlBlendState();
            result.Unk1 = parser.ReadInt32();      
            result.Unk2 = parser.ReadInt32();      
            result.BlendFactor1 = parser.ReadInt32();      
            result.Unk4 = parser.ReadInt32();      
            result.Unk5 = parser.ReadInt32();      
            result.BlendFactor2 = parser.ReadInt32();      
            result.Unk7 = parser.ReadInt32();      
            result.Unk8 = parser.ReadInt32();      
            result.Unk9 = parser.ReadInt32();      
            result.Unk10 = parser.ReadInt32();      
            result.Unk11 = parser.ReadInt32();      
            result.Unk12 = parser.ReadInt32();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlAttribute.MatlRasterizerState ParseMatlRasterizerState(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlAttribute.MatlRasterizerState();
            result.FillMode = parser.ReadInt32();      
            result.CullMode = parser.ReadInt32();      
            result.DepthBias = parser.ReadSingle();      
            result.Unk4 = parser.ReadSingle();      
            result.Unk5 = parser.ReadSingle();      
            result.Unk6 = parser.ReadInt32();      
            result.Unk7 = parser.ReadInt32();      
            result.Unk8 = parser.ReadSingle();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlAttribute.MatlSampler ParseMatlSampler(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlAttribute.MatlSampler();
            result.WrapS = parser.ReadInt32();      
            result.WrapT = parser.ReadInt32();      
            result.WrapR = parser.ReadInt32();      
            result.MinFilter = parser.ReadInt32();      
            result.MagFilter = parser.ReadInt32();      
            result.Unk6 = parser.ReadInt32();      
            result.Unk7 = parser.ReadInt32();      
            result.Unk8 = parser.ReadInt32();      
            result.Unk9 = parser.ReadInt32();      
            result.Unk10 = parser.ReadInt32();      
            result.Unk11 = parser.ReadInt32();      
            result.Unk12 = parser.ReadInt32();      
            result.LodBias = parser.ReadSingle();      
            result.MaxAnisotropy = parser.ReadInt32();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlAttribute.MatlString ParseMatlString(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlAttribute.MatlString();
            result.Text = parser.ReadOffsetReadString();

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlAttribute.MatlUvTransform ParseMatlUvTransform(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlAttribute.MatlUvTransform();
            result.X = parser.ReadSingle();      
            result.Y = parser.ReadSingle();      
            result.Z = parser.ReadSingle();      
            result.W = parser.ReadSingle();      
            result.V = parser.ReadSingle();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

        public static Formats.Materials.MatlAttribute.MatlVector4 ParseMatlVector4(this SsbhParser parser)
        {
            var result = new Formats.Materials.MatlAttribute.MatlVector4();
            result.X = parser.ReadSingle();      
            result.Y = parser.ReadSingle();      
            result.Z = parser.ReadSingle();      
            result.W = parser.ReadSingle();      

            long temp = parser.Position;
            result.PostProcess(parser);
            parser.Seek(temp);

            return result;
        }

    }
}
	
   