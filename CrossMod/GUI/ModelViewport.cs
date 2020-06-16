﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CrossMod.Rendering;
using SFGraphics.Cameras;
using SFGraphics.GLObjects.GLObjectManagement;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Input;
using CrossMod.Nodes;
using CrossMod.Rendering.Models;
using SFGraphics.GLObjects.Framebuffers;
using SFGraphics.GLObjects.Shaders;

namespace CrossMod.GUI
{
    public partial class ModelViewport : UserControl
    {
        private AnimationBar animationBar;

        private readonly HashSet<string> renderableNodeNames = new HashSet<string>();

        // This isn't a dictionary so that render order is preserved.
        private readonly List<IRenderable> renderableNodes = new List<IRenderable>();

        private IRenderable renderTexture = null;

        private readonly Camera camera = new Camera() { FarClipPlane = 500000 };
        private Vector2 mousePosition;
        private float mouseScrollWheel;

        public bool HasAnimation => animationBar.Animation != null;
        public IRenderableAnimation RenderableAnimation
        {
            set
            {
                animationBar.Animation = value;
                controlBox.Visible = true;
            }
        }

        public ScriptNode ScriptNode
        {
            get => animationBar.ScriptNode;
            set => animationBar.ScriptNode = value;
        }

        public ModelViewport()
        {
            InitializeComponent();
            AddAnimationBar();
            CreateRenderFrameEvents();
        }

        public void RestartRendering()
        {
            glViewport.RestartRendering();
        }

        public void PauseRendering()
        {
            glViewport.PauseRendering();
        }

        public void UpdateTexture(NutexNode texture)
        {
            var wasRendering = glViewport.IsRendering;
            PauseRendering();

            var node = texture?.GetRenderableNode();
            renderTexture = node;

            if (wasRendering)
                RestartRendering();
        }

        public void AddRenderableNode(string name, IRenderableNode value)
        {
            var wasRendering = glViewport.IsRendering;

            // Make sure the context is current on this thread.
            PauseRendering();

            ClearBonesAndMeshList();

            if (value == null)
                return;

            var newNode = value.GetRenderableNode();

            // Prevent duplicates. Paths should be unique.
            if (!renderableNodeNames.Contains(name))
            {
                renderableNodes.Add(newNode);
                renderableNodeNames.Add(name);
            }

            // Duplicate nodes should still update the mesh list.
            if (newNode is RSkeleton skeleton)
            {
                DisplaySkeleton(skeleton);
            }
            else if (newNode is IRenderableModel renderableModel)
            {
                DisplayMeshes(renderableModel.GetModel());
                DisplaySkeleton(renderableModel.GetSkeleton());
            }

            if (value is NumdlNode)
            {
                FrameSelection();
            }

            if (wasRendering)
                RestartRendering();
        }

        public void FrameSelection()
        {
            // Bounding spheres will help account for the vastly different model sizes.
            var spheres = new List<Vector4>();
            foreach (var node in renderableNodes)
            {
                if (node is Rnumdl rnumdl && rnumdl.Model != null)
                {
                    spheres.Add(rnumdl.Model.BoundingSphere);
                }
            }

            var allModelBoundingSphere = SFGraphics.Utils.BoundingSphereGenerator.GenerateBoundingSphere(spheres);
            camera.FrameBoundingSphere(allModelBoundingSphere, 0);
        }

        public void ClearFiles()
        {
            // Pause frame updates so we don't access the render nodes while clearing them.
            bool wasRendering = glViewport.IsRendering;
            glViewport.PauseRendering();

            animationBar.Clear();

            renderableNodes.Clear();
            renderableNodeNames.Clear();

            meshList.Clear();
            boneTree.Nodes.Clear();

            ParamNodeContainer.HitData = new Collision[0];

            GC.WaitForPendingFinalizers();
            GLObjectManager.DeleteUnusedGLObjects();

            if (wasRendering)
                glViewport.RestartRendering();
        }

        public System.Drawing.Bitmap GetScreenshot()
        {
            return Framebuffer.ReadDefaultFramebufferImagePixels(glViewport.Width, glViewport.Height, true);
        }

        public void SaveScreenshot(string filePath)
        {
            glViewport.PauseRendering();
            Framebuffer.ReadDefaultFramebufferImagePixels(glViewport.Width, glViewport.Height, false).Save(filePath);
            glViewport.RestartRendering();
        }

        public CameraControl GetCameraControl()
        {
            return new CameraControl(camera);
        }

        public void Close()
        {
            glViewport.Dispose();
        }

        public void HideExpressionMeshes()
        {
            string[] expressionPatterns = { "Blink", "Attack", "Ouch", "Talk",
                "Capture", "Ottotto", "Escape", "Half",
                "Pattern", "Result", "Harf", "Hot", "Heavy",
                "Voice", "Fura", "Catch", "Cliff", "FLIP",
                "Bound", "Down", "Final", "Result", "StepPose",
                "Sorori", "Fall", "Appeal", "Damage", "CameraHit", "laugh", 
                "breath", "swell", "_low", "_bink", "inkMesh" };

            // TODO: This is probably not a very efficient way of doing this.
            foreach (ListViewItem item in meshList.Items)
            {
                var itemName = item.Name.ToLower();
                foreach (var pattern in expressionPatterns)
                {
                    if (itemName.Contains("openblink") || itemName.Contains("belly_low") || itemName.Contains("facen"))
                        continue;

                    if (itemName.Contains(pattern.ToLower()))
                    {
                        item.Checked = false;
                        ((RMesh)item.Tag).Visible = false;
                    }
                }
            }
        }


        public void RenderAnimationToFolder(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                return;

            glViewport.PauseRendering();
            animationBar.Stop();

            try
            {
                int repeat = (int)(1 / RenderSettings.Instance.RenderSpeed);
                for (int i = 0; i <= animationBar.FrameCount; ++i)
                {
                    animationBar.Frame = i;
                    glViewport.RenderFrame();
                    for (int j = 1; j <= repeat; ++j)
                    {
                        Framebuffer.ReadDefaultFramebufferImagePixels(glViewport.Width, glViewport.Height, false)
                                   .Save($"{folderPath}//Frame {i}.png");
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error while exporting frames");
                Console.Error.WriteLine(e.ToString());
            }

            glViewport.RestartRendering();
        }

        public async System.Threading.Tasks.Task RenderAnimationToGifAsync(string outputPath, IProgress<int> progress)
        {
            if (string.IsNullOrEmpty(outputPath))
                return;

            // Disable automatic updates so frames can be rendered manually.
            glViewport.PauseRendering();
            animationBar.Stop();

            var frames = new List<System.Drawing.Bitmap>(animationBar.FrameCount);

            // Rendering can't happen on a separate thread
            try
            {
                int repeat = (int)(1 / RenderSettings.Instance.RenderSpeed);
                for (int i = 0; i <= animationBar.FrameCount; ++i)
                {
                    animationBar.Frame = i;
                    glViewport.RenderFrame();
                    for (int j = 1; j <= repeat; ++j)
                    {
                        frames.Add(Framebuffer.ReadDefaultFramebufferImagePixels(glViewport.Width, glViewport.Height, false));
                    }

                    var ratio = (double) i / animationBar.FrameCount;
                    progress.Report((int)(ratio * 100));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error while making GIF");
                Console.Error.WriteLine(e.ToString());
            }

            glViewport.RestartRendering();

            // Continue on separate thread to maintain responsiveness.
            await System.Threading.Tasks.Task.Run(() =>
            {
                using (var gif = new AnimatedGif.AnimatedGifCreator(outputPath, 20, 0))
                {
                    for (int i = 0; i < frames.Count; i++)
                        gif.AddFrame(frames[i], -1, AnimatedGif.GifQuality.Bit8);
                }
            });

            foreach (var frame in frames)
            {
                frame.Dispose();
            }
        }

        private void AddAnimationBar()
        {
            animationBar = new AnimationBar
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
            };
            controlBox.Controls.Add(animationBar);
        }

        private void CreateRenderFrameEvents()
        {
            glViewport.RenderFrameInterval = 15;
            glViewport.VSync = false;
            glViewport.OnRenderFrame += RenderNodes;
            glViewport.RestartRendering();
        }

        /// <summary>
        /// Populates the meshes tab, and binds the given model to subcomponents such as the animation bar.
        /// </summary>
        /// <param name="model"></param>
        private void DisplayMeshes(RModel model)
        {
            animationBar.Model = model;

            if (model != null)
            {
                meshList.Items.Clear();

                foreach (var mesh in model.subMeshes)
                {
                    ListViewItem item = new ListViewItem
                    {
                        Name = mesh.Name,
                        Text = mesh.Name,
                        Tag = mesh,
                        Checked = mesh.Visible
                    };
                    meshList.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Populates the bones tab, and binds the given skeleton to subcomponents such as the animation bar
        /// </summary>
        /// <param name="skeleton"></param>
        private void DisplaySkeleton(RSkeleton skeleton)
        {
            if (skeleton == null)
                return;

            animationBar.Skeleton = skeleton;
            Dictionary<int, TreeNode> boneById = new Dictionary<int, TreeNode>();

            foreach(RBone b in skeleton.Bones)
            {
                TreeNode node = new TreeNode
                {
                    Text = b.Name
                };

                boneById.Add(b.Id, node);
                if (b.ParentId == -1)
                    boneTree.Nodes.Add(node);
            }

            foreach (RBone b in skeleton.Bones)
            {
                if (b.ParentId != -1)
                    boneById[b.ParentId].Nodes.Add(boneById[b.Id]);
            }
        }

        private void ClearBonesAndMeshList()
        {
            boneTree.Nodes.Clear();
            meshList.Items.Clear();
            controlBox.Visible = false;
        }

        public void RenderFrame()
        {
            if (!glViewport.IsDisposed)
                glViewport.RenderFrame();
        }

        public void BeginBatchRenderMode()
        {
            glViewport.PauseRendering();
            boneTree.Visible = false;
            meshList.Visible = false;
        }

        public void EndBatchRenderMode()
        {
            glViewport.RestartRendering();
            boneTree.Visible = true;
            meshList.Visible = true;
        }

        private void RenderNodes(object sender, EventArgs e)
        {
            // Ensure shaders are created before drawing anything.
            if (!ShaderContainer.HasSetUp)
                ShaderContainer.SetUpShaders();

            SetUpViewport();


            if (renderTexture != null)
            {
                renderTexture.Render(camera);
            }            
            else
            {
                foreach (var node in renderableNodes)
                    node.Render(camera);

                ParamNodeContainer.Render(camera);
                ScriptNode?.Render(camera);
            }
        }

        private void SetUpViewport()
        {
            ClearViewportBuffers();
            SetRenderState();
            UpdateCamera();

            if (RenderSettings.Instance.RenderGrid)
                FloorDrawing.DrawFloor(camera.MvpMatrix);

            if (RenderSettings.Instance.RenderAxis)
                FloorDrawing.DrawAxis(camera.MvpMatrix);
        }

        private static void ClearViewportBuffers()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            if (RenderSettings.Instance.RenderBackground)
                GL.ClearColor(0.25f, 0.25f, 0.25f, 1);
            else
                GL.ClearColor(0f, 0f, 0f, 1);
        }

        private static void SetRenderState()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
        }

        private void UpdateCamera()
        {
            // Accessing the control properties can't be done on another thread.
            glViewport.BeginInvoke(new Action(() =>
            {
                var mouseState = Mouse.GetState();
                var keyboardState = Keyboard.GetState();

                Vector2 newMousePosition = new Vector2(mouseState.X, mouseState.Y);
                float newMouseScrollWheel = mouseState.Wheel;

                // Reduce the chance of rotating the viewport while the mouse is on other controls.
                if (glViewport.Focused && glViewport.ClientRectangle.Contains(PointToClient(MousePosition)))
                {
                    if (mouseState.IsButtonDown(MouseButton.Left))
                    {
                        camera.RotationXRadians += (newMousePosition.Y - mousePosition.Y) / 100f;
                        camera.RotationYRadians += (newMousePosition.X - mousePosition.X) / 100f;
                    }
                    if (mouseState.IsButtonDown(MouseButton.Right))
                    {
                        camera.Pan(newMousePosition.X - mousePosition.X, newMousePosition.Y - mousePosition.Y);
                    }
                    if (keyboardState.IsKeyDown(Key.W))
                        camera.Zoom(0.5f);
                    if (keyboardState.IsKeyDown(Key.S))
                        camera.Zoom(-0.5f);

                    camera.Zoom((newMouseScrollWheel - mouseScrollWheel) * 0.1f);
                }

                mousePosition = newMousePosition;
                mouseScrollWheel = newMouseScrollWheel;
            }));

        }

        private void glViewport_Resize(object sender, EventArgs e)
        {
            // Adjust for changing render dimensions.
            camera.RenderWidth = glViewport.Width;
            camera.RenderHeight = glViewport.Height;

            glViewport.RenderFrame();
        }

        private void meshList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item == null || !(e.Item.Tag is RMesh))
                return;

            ((RMesh)e.Item.Tag).Visible = e.Item.Checked;
        }
    }
}
