using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FDS.GlitchShader
{
    public class GlitchRenderPassFeature : ScriptableRendererFeature
    {
        #region NestedType

        class CustomRenderPass : ScriptableRenderPass
        {
            #region Fields

            public RenderTargetIdentifier Source;

            private Material material;
            private RenderTargetHandle tempRenderTargetHandle;

            #endregion

            #region Conctruction

            public CustomRenderPass(Settings settings)
            {
                this.material = settings.material;
                tempRenderTargetHandle.Init("_TemporaryColorTexture");
            }

            #endregion

            #region PublicMethods

            // This method is called before executing the render pass.
            // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
            // When empty this render pass will render to the active camera render target.
            // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
            // The render pipeline will ensure target setup and clearing happens in a performant manner.
            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
            }

            // Here you can implement the rendering logic.
            // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
            // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
            // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var commandBuffer = CommandBufferPool.Get();
                commandBuffer.GetTemporaryRT(tempRenderTargetHandle.id, renderingData.cameraData.cameraTargetDescriptor);

                Blit(commandBuffer, Source, tempRenderTargetHandle.Identifier(), material);
                Blit(commandBuffer, tempRenderTargetHandle.Identifier(), Source);

                context.ExecuteCommandBuffer(commandBuffer);
                CommandBufferPool.Release(commandBuffer);
            }

            // Cleanup any allocated resources that were created during the execution of this render pass.
            public override void OnCameraCleanup(CommandBuffer cmd)
            {
            }

            #endregion
        }

        [System.Serializable]
        public class Settings
        {
            #region Fields

            public Material material = null;

            #endregion
        }

        #endregion

        #region Fields

        public Settings SettingsMaterial = new Settings();
        private CustomRenderPass _scriptablePass;

        #endregion

        #region PublicMethods

        /// <inheritdoc/>
        public override void Create()
        {
            _scriptablePass = new CustomRenderPass(SettingsMaterial);
            _scriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        // Here you can inject one or multiple render passes in the renderer.
        // This method is called when setting up the renderer once per-camera.
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _scriptablePass.Source = renderer.cameraColorTarget;
            renderer.EnqueuePass(_scriptablePass);
        }

        #endregion
    }
}
