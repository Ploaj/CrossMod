﻿using CrossMod.Rendering;
using CrossMod.Rendering.GlTools;
using CrossMod.Tools;
using CrossModGui.Tools;
using SSBHLib.Formats.Materials;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace CrossModGui.ViewModels.MaterialEditor
{
    public partial class MaterialEditorWindowViewModel : ViewModelBase
    {
        public ObservableCollection<MaterialCollection> MaterialCollections { get; } = new ObservableCollection<MaterialCollection>();

        public MaterialCollection? CurrentMaterialCollection { get; set; }
        public Material? CurrentMaterial { get; set; }

        public class MaterialSaveEventArgs : System.EventArgs
        {
            public MaterialCollection MaterialCollection { get; }
            public string FilePath { get; }

            public MaterialSaveEventArgs(MaterialCollection materialCollection, string filePath)
            {
                MaterialCollection = materialCollection;
                FilePath = filePath;
            }
        }

        public event System.EventHandler<MaterialSaveEventArgs>? MatlSaving;

        public ObservableCollection<string> PossibleTextureNames { get; } = new ObservableCollection<string>();

        public Dictionary<MatlCullMode, string> DescriptionByCullMode { get; } = new Dictionary<MatlCullMode, string>
        {
            { MatlCullMode.Back, "Back" },
            { MatlCullMode.Front, "Front" },
            { MatlCullMode.None, "None" },
        };

        public Dictionary<MatlBlendFactor, string> DescriptionByBlendFactor { get; } = new Dictionary<MatlBlendFactor, string>
        {
            { MatlBlendFactor.Zero, "Zero" },
            { MatlBlendFactor.One, "One" },
            { MatlBlendFactor.SourceAlpha, "Source Alpha" },
            { MatlBlendFactor.DestinationAlpha, "Destination Alpha" },
            { MatlBlendFactor.SourceColor, "Source Color" },
            { MatlBlendFactor.DestinationColor, "Destination Color" },
            { MatlBlendFactor.OneMinusSourceAlpha, "One Minus Source Alpha" },
            { MatlBlendFactor.OneMinusDestinationAlpha, "One Minus Destination Alpha" },
            { MatlBlendFactor.OneMinusSourceColor, "One Minus Source Color" },
            { MatlBlendFactor.OneMinusDestinationColor, "One Minus Destination Color" },
            { MatlBlendFactor.SourceAlphaSaturate, "Source Alpha Saturate" },
        };

        public Dictionary<int, string> DescriptionByAnisotropy { get; } = new Dictionary<int, string>
        {
            { 0, "1x" },
            { 2, "2x" },
            { 4, "4x" },
            { 8, "16x" },
            { 16, "128x" },
        };

        public Dictionary<MatlFillMode, string> DescriptionByFillMode { get; } = new Dictionary<MatlFillMode, string>
        {
            { MatlFillMode.Solid, "Solid" },
            { MatlFillMode.Line, "Line" },
        };

        public Dictionary<MatlMagFilter, string> DescriptionByMagFilter { get; } = new Dictionary<MatlMagFilter, string>
        {
            { MatlMagFilter.Nearest, "Nearest" },
            { MatlMagFilter.Linear, "Linear" },
            { MatlMagFilter.Linear2, "Linear + ???" },
        };

        public Dictionary<MatlMinFilter, string> DescriptionByMinFilter { get; } = new Dictionary<MatlMinFilter, string>
        {
            { MatlMinFilter.Nearest, "Nearest" },
            { MatlMinFilter.LinearMipmapLinear, "Linear Mipmap Linear" },
            { MatlMinFilter.LinearMipmapLinear2, "Linear Mipmap Linear2" },
        };

        public Dictionary<FilteringType, string> DescriptionByFilteringType { get; } = new Dictionary<FilteringType, string>
        {
            { FilteringType.Default, "Default" },
            { FilteringType.Default2, "Default2" },
            { FilteringType.AnisotropicFiltering, "Anisotropic Filtering" },
        };

        public Dictionary<MatlWrapMode, string> DescriptionByWrapMode { get; } = new Dictionary<MatlWrapMode, string>
        {
            { MatlWrapMode.Repeat, "Repeat" },
            { MatlWrapMode.ClampToEdge, "Clamp to Edge" },
            { MatlWrapMode.MirroredRepeat, "Mirrored Repeat" },
            { MatlWrapMode.ClampToBorder, "Clamp to Border" },
        };

        public MaterialEditorWindowViewModel(IEnumerable<System.Tuple<string, RNumdl>> rnumdls)
        {
            // TODO: Restrict the textures used for cube maps.
            foreach (var name in TextureAssignment.defaultTexturesByName.Keys)
                PossibleTextureNames.Add(name);

            // Group materials by matl.
            foreach (var pair in rnumdls)
            {
                var name = pair.Item1;
                var rnumdl = pair.Item2;

                if (rnumdl.Matl == null)
                    continue;

                // TODO: Each material should have a different set of available texture names.
                PossibleTextureNames.AddRange(rnumdl.TextureByName.Keys);

                var collection = CreateMaterialCollection(name, rnumdl.MaterialByName, rnumdl.Matl);
                MaterialCollections.Add(collection);
            }

            // Find the correct matl to update when saving.
            MatlSaving += (s, e) =>
            {
                var matl = rnumdls.SingleOrDefault(r => r.Item1 == e.MaterialCollection.Name).Item2.Matl;
                if (matl != null)
                    UpdateAndSaveMatl(matl, e.MaterialCollection, e.FilePath);
            };
        }

        private void UpdateAndSaveMatl(Matl matl, MaterialCollection collection, string outputPath)
        {
            // TODO: Completely recreate the Matl from the view model.

            foreach (var entry in matl.Entries)
            {
                var material = collection.Materials.SingleOrDefault(m => m.ShaderLabel == entry.ShaderLabel && m.Name == entry.MaterialLabel);
                if (material == null)
                    continue;

                UpdateAttributes(entry, material);
            }

            SSBHLib.Ssbh.TrySaveSsbhFile(outputPath, matl);
        }

        private void UpdateAttributes(MatlEntry entry, Material material)
        {
            foreach (var attribute in entry.Attributes)
            {
                // The data type isn't known, so check each type.
                switch (attribute.DataType)
                {
                    case MatlEnums.ParamDataType.Float:
                        UpdateMatlFloat(material, attribute);
                        break;
                    case MatlEnums.ParamDataType.Boolean:
                        UpdateMatlBoolean(material, attribute);
                        break;
                    case MatlEnums.ParamDataType.String:
                        UpdateMatlTexture(material, attribute);
                        break;
                    case MatlEnums.ParamDataType.Vector4:
                        UpdateMatlVec4(material, attribute);
                        break;
                    // TODO: Fully represent these types in the viewmodel.
                    case MatlEnums.ParamDataType.Sampler:
                        UpdateMatlSampler(material, attribute);
                        break;
                    case MatlEnums.ParamDataType.RasterizerState:
                        UpdateMatlRasterizerState(material, attribute);
                        break;
                    case MatlEnums.ParamDataType.BlendState:
                        UpdateMatlBlendState(material, attribute);
                        break;
                }
            }
        }

        private static void UpdateMatlBlendState(Material material, MatlAttribute attribute)
        {
            if (attribute.DataObject is MatlAttribute.MatlBlendState blendState)
                attribute.DataObject = GetBlendState(material, blendState);
        }

        private static void UpdateMatlRasterizerState(Material material, MatlAttribute attribute)
        {
            if (attribute.DataObject is MatlAttribute.MatlRasterizerState rasterizerState)
                attribute.DataObject = GetRasterizerState(material, rasterizerState);
        }

        private static void UpdateMatlSampler(Material material, MatlAttribute attribute)
        {
            var samplerParam = material.TextureParams.FirstOrDefault(p => p.SamplerParamId == attribute.ParamId.ToString());
            if (attribute.DataObject is MatlAttribute.MatlSampler matlSampler)
                attribute.DataObject = GetMatlSampler(samplerParam, matlSampler);
        }

        private static void UpdateMatlVec4(Material material, MatlAttribute attribute)
        {
            var vec4Param = material.Vec4Params.FirstOrDefault(p => p.ParamId == attribute.ParamId.ToString());
            attribute.DataObject = GetMatlVec4(vec4Param);
        }

        private static void UpdateMatlFloat(Material material, MatlAttribute attribute)
        {
            var floatParam = material.FloatParams.FirstOrDefault(p => p.ParamId == attribute.ParamId.ToString());
            attribute.DataObject = floatParam.Value;
        }

        private static void UpdateMatlBoolean(Material material, MatlAttribute attribute)
        {
            var boolparam = material.BooleanParams.FirstOrDefault(p => p.ParamId == attribute.ParamId.ToString());
            attribute.DataObject = boolparam.Value;
        }

        private static void UpdateMatlTexture(Material material, MatlAttribute attribute)
        {
            var textureParam = material.TextureParams.FirstOrDefault(p => p.ParamId == attribute.ParamId.ToString());
            attribute.DataObject = new MatlAttribute.MatlString { Text = textureParam.Value };
        }

        private MaterialCollection CreateMaterialCollection(string name, Dictionary<string,RMaterial> materialByName, Matl matl)
        {
            var collection = new MaterialCollection { Name = name };
            for (int i = 0; i < matl.Entries.Length; i++)
            {
                // Pass a reference to the render material to enable real time updates.
                materialByName.TryGetValue(matl.Entries[i].MaterialLabel, out RMaterial? rMaterial);

                var material = CreateMaterial(matl.Entries[i], i, rMaterial);
                collection.Materials.Add(material);
            }

            return collection;
        }

        private Material CreateMaterial(MatlEntry entry, int index, RMaterial? rMaterial)
        {
            var idColor = UniqueColors.IndexToColor(index);

            var material = new Material
            {
                Name = entry.MaterialLabel,
                ShaderLabel = entry.ShaderLabel,
                MaterialIdColor = new SolidColorBrush(Color.FromArgb(255,
                    (byte)idColor.X,
                    (byte)idColor.Y,
                    (byte)idColor.Z)),
            };

            UpdateMaterialFromEntry(entry, material);

            // Enable real time viewport updates.
            if (rMaterial !=  null)
            {
                SyncBooleans(rMaterial, material, entry);
                SyncFloats(rMaterial, material, entry);
                SyncTexturesSamplers(rMaterial, material, entry);
                SyncVectors(rMaterial, material, entry);

                // Rasterizer state and blend state are stored with the material
                // because there is only a single rasterizer state and blend state.

                material.PropertyChanged += (s, e) =>
                {
                    var rasterizerState = entry.Attributes.FirstOrDefault(a => a.ParamId == MatlEnums.ParamId.RasterizerState0);
                    var blendState = entry.Attributes.FirstOrDefault(a => a.ParamId == MatlEnums.ParamId.BlendState0);

                    switch (e.PropertyName)
                    {
                        case nameof(material.CullMode):
                            rMaterial.CullMode = material.CullMode.ToOpenTk();
                            if (rasterizerState != null)
                                UpdateMatlRasterizerState(material, rasterizerState);
                            break;
                        case nameof(material.FillMode):
                            rMaterial.FillMode = material.FillMode.ToOpenTk();
                            if (rasterizerState != null)
                                UpdateMatlRasterizerState(material, rasterizerState);
                            break;
                        case nameof(material.SourceColor):
                            rMaterial.SourceColor = material.SourceColor.ToOpenTk();
                            if (blendState != null)
                                UpdateMatlBlendState(material, blendState);
                            break;
                        case nameof(material.DestinationColor):
                            rMaterial.DestinationColor = material.DestinationColor.ToOpenTk();
                            if (blendState != null)
                                UpdateMatlBlendState(material, blendState);
                            break;
                    }
                };

            }

            return material;
        }

        private static void SyncBooleans(RMaterial rMaterial, Material material, MatlEntry entry)
        {
            foreach (var param in material.BooleanParams)
            {
                if (!System.Enum.TryParse(param.ParamId, out MatlEnums.ParamId paramId))
                    continue;

                param.PropertyChanged += (s, e) => rMaterial.UpdateBoolean(paramId, param.Value);
                param.PropertyChanged += (s, e) =>
                {
                    var attribute = entry.Attributes.FirstOrDefault(a => a.ParamId == paramId);
                    if (attribute != null)
                        UpdateMatlBoolean(material, attribute);
                };
            }
        }

        private static void SyncFloats(RMaterial rMaterial, Material material, MatlEntry entry)
        {
            foreach (var param in material.FloatParams)
            {
                if (!System.Enum.TryParse(param.ParamId, out MatlEnums.ParamId paramId))
                    continue;

                param.PropertyChanged += (s, e) => rMaterial.UpdateFloat(paramId, param.Value);
                param.PropertyChanged += (s, e) =>
                {
                    var attribute = entry.Attributes.FirstOrDefault(a => a.ParamId == paramId);
                    if (attribute != null)
                        UpdateMatlFloat(material, attribute);
                };
            }
        }

        private static void SyncTexturesSamplers(RMaterial rMaterial, Material material, MatlEntry entry)
        {
            foreach (var param in material.TextureParams)
            {
                if (!System.Enum.TryParse(param.ParamId, out MatlEnums.ParamId paramId))
                    continue;

                if (!System.Enum.TryParse(param.SamplerParamId, out MatlEnums.ParamId samplerParamId))
                    continue;

                param.PropertyChanged += (s, e) => 
                { 
                    rMaterial.UpdateTexture(paramId, param.Value);
                    rMaterial.UpdateSampler(samplerParamId, CreateSampler(param));
                };
                param.PropertyChanged += (s, e) =>
                {
                    var attribute = entry.Attributes.FirstOrDefault(a => a.ParamId == paramId);
                    if (attribute != null)
                        UpdateMatlTexture(material, attribute);
                    var samplerAttribute = entry.Attributes.FirstOrDefault(a => a.ParamId == samplerParamId);
                    if (samplerAttribute != null)
                        UpdateMatlSampler(material, attribute);
                };
            }
        }

        private static void SyncVectors(RMaterial rMaterial, Material material, MatlEntry entry)
        {
            foreach (var param in material.Vec4Params)
            {
                if (!System.Enum.TryParse(param.ParamId, out MatlEnums.ParamId paramId))
                    continue;

                param.PropertyChanged += (s, e) => rMaterial.UpdateVec4(paramId, new OpenTK.Vector4(param.Value1, param.Value2, param.Value3, param.Value4));
                param.PropertyChanged += (s, e) =>
                {
                    var attribute = entry.Attributes.FirstOrDefault(a => a.ParamId == paramId);
                    if (attribute != null)
                        UpdateMatlVec4(material, attribute);
                };
            }
        }

        private static SamplerData CreateSampler(TextureParam param)
        {
            return new SamplerData
            {
                MinFilter = param.MinFilter.ToOpenTk(),
                MagFilter = param.MagFilter.ToOpenTk(),
                WrapS = param.WrapS.ToOpenTk(),
                WrapT = param.WrapT.ToOpenTk(),
                WrapR = param.WrapR.ToOpenTk(),
                LodBias = param.LodBias,
                // Disable anisotropic filtering if it's disabled in the material.
                MaxAnisotropy = param.TextureFilteringType == FilteringType.AnisotropicFiltering ? param.MaxAnisotropy : 1,       
            };
        }

        private static void UpdateMaterialFromEntry(MatlEntry entry, Material material)
        {
            // There should only be a single rasterizer state in each material.
            if (entry.GetRasterizerStates().TryGetValue(MatlEnums.ParamId.RasterizerState0, out MatlAttribute.MatlRasterizerState? rasterizerState))
            {
                material.CullMode = rasterizerState.CullMode;
                material.FillMode = rasterizerState.FillMode;
            }

            // There should only be a single blend state in each material.
            if (entry.GetBlendStates().TryGetValue(MatlEnums.ParamId.BlendState0, out MatlAttribute.MatlBlendState? blendState))
            {
                material.SourceColor = blendState.SourceColor;
                material.DestinationColor = blendState.DestinationColor;
            }

            material.BooleanParams.AddRange(entry.GetBools()
                .Select(b => new BooleanParam { ParamId = b.Key.ToString(), Value = b.Value }));

            material.FloatParams.AddRange(entry.GetFloats()
                .Select(f => new FloatParam { ParamId = f.Key.ToString(), Value = f.Value }));

            material.Vec4Params.AddRange(entry.GetVectors()
                .Select(v => new Vec4Param
                {
                    ParamId = v.Key.ToString(),
                    Value1 = v.Value.X,
                    Value2 = v.Value.Y,
                    Value3 = v.Value.Z,
                    Value4 = v.Value.W
                }));

            // Set descriptions and GUI info.
            foreach (var param in material.Vec4Params)
            {
                TryAssignValuesFromDescription(param);
            }

            // TODO: Are texture names case sensitive?
            material.TextureParams.AddRange(entry.GetTextures()
                .Select(t => new TextureParam { ParamId = t.Key.ToString(), Value = t.Value.ToLower(), SamplerParamId = ParamIdExtensions.GetSampler(t.Key).ToString() }));

            UpdateTextureParamsFromSamplers(entry, material);
        }

        private static void UpdateTextureParamsFromSamplers(MatlEntry entry, Material material)
        {
            var entrySamplers = entry.GetSamplers();
            foreach (var param in material.TextureParams)
            {
                if (!System.Enum.TryParse(param.ParamId, out MatlEnums.ParamId textureId))
                    continue;

                if (!entrySamplers.TryGetValue(ParamIdExtensions.GetSampler(textureId), out MatlAttribute.MatlSampler? sampler))
                    continue;

                param.WrapS = sampler.WrapS;
                param.WrapT = sampler.WrapT;
                param.WrapR = sampler.WrapR;
                param.MinFilter = sampler.MinFilter;
                param.MagFilter = sampler.MagFilter;
                param.LodBias = sampler.LodBias;
                param.MaxAnisotropy = sampler.MaxAnisotropy;
                param.TextureFilteringType = sampler.TextureFilteringType;
            }
        }

        public void SaveMatl(string outputPath)
        {
            if (CurrentMaterialCollection != null)
                MatlSaving?.Invoke(this, new MaterialSaveEventArgs(CurrentMaterialCollection, outputPath));    
        }

        private static MatlAttribute.MatlBlendState GetBlendState(Material material, MatlAttribute.MatlBlendState previous)
        {
            // TODO: Completely remake the data object.
            // TODO: This modifies the previous object.
            previous.SourceColor = material.SourceColor;
            previous.DestinationColor = material.DestinationColor;
            return previous;
        }

        private static MatlAttribute.MatlRasterizerState GetRasterizerState(Material material, MatlAttribute.MatlRasterizerState previous)
        {
            // TODO: Completely remake the data object.
            // TODO: This modifies the previous object.
            previous.CullMode = material.CullMode;
            previous.FillMode = material.FillMode;
            return previous;
        }

        private static MatlAttribute.MatlVector4 GetMatlVec4(Vec4Param param)
        {
            return new MatlAttribute.MatlVector4
            {
                X = param.Value1,
                Y = param.Value2,
                Z = param.Value3,
                W = param.Value4
            };
        }

        private static MatlAttribute.MatlSampler GetMatlSampler(TextureParam param, MatlAttribute.MatlSampler previous)
        {
            // TODO: Completely remake the data object.
            // TODO: This modifies the previous object.
            previous.WrapS = param.WrapS;
            previous.WrapT = param.WrapT;
            previous.WrapR = param.WrapR;
            previous.MagFilter = param.MagFilter;
            previous.MinFilter = param.MinFilter;
            previous.LodBias = param.LodBias;
            previous.MaxAnisotropy = param.MaxAnisotropy;
            previous.TextureFilteringType = param.TextureFilteringType;

            return previous;
        }

        private static bool TryAssignValuesFromDescription(Vec4Param vec4Param)
        {
            if (MaterialParamDescriptions.Instance.ParamDescriptionsByName.TryGetValue(vec4Param.ParamId,
                out MaterialParamDescriptions.ParamDescription? description))
            {
                vec4Param.Label1 = description.Label1 ?? "Unused";
                vec4Param.Min1 = description.Min1.GetValueOrDefault(0);
                vec4Param.Max1 = description.Max1.GetValueOrDefault(1);

                vec4Param.Label2 = description.Label2 ?? "Unused";
                vec4Param.Min2 = description.Min2.GetValueOrDefault(0);
                vec4Param.Max2 = description.Max2.GetValueOrDefault(1);

                vec4Param.Label3 = description.Label3 ?? "Unused";
                vec4Param.Min3 = description.Min3.GetValueOrDefault(0);
                vec4Param.Max3 = description.Max3.GetValueOrDefault(1);

                vec4Param.Label4 = description.Label4 ?? "Unused";
                vec4Param.Min4 = description.Min4.GetValueOrDefault(0);
                vec4Param.Max4 = description.Max4.GetValueOrDefault(1);
                return true;
            }

            return false;
        }
    }
}
