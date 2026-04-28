using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelateFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material pixelateMaterial;
        public float blockSize = 4f;
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public Settings settings = new Settings();
    private PixelatePass pixelatePass;

    public override void Create()
    {
        // This links the feature to the pass below
        pixelatePass = new PixelatePass(settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.pixelateMaterial == null) return;

        pixelatePass.renderPassEvent = settings.renderPassEvent;
        renderer.EnqueuePass(pixelatePass);
    }
}

// THIS IS THE PART THAT WAS MISSING
public class PixelatePass : ScriptableRenderPass
{
    private PixelateFeature.Settings settings;
    private RTHandle tempRT;
    private const string profilerTag = "Pixelate Pass";

    public PixelatePass(PixelateFeature.Settings settings)
    {
        this.settings = settings;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
        desc.depthBufferBits = 0;
        RenderingUtils.ReAllocateIfNeeded(ref tempRT, desc, name: "_TempPixelateRT");
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (settings.pixelateMaterial == null) return;

        CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

        // This sends the block size from the inspector to your shader
        settings.pixelateMaterial.SetFloat("_BlockSize", settings.blockSize);

        RTHandle source = renderingData.cameraData.renderer.cameraColorTargetHandle;

        // Blit draws the screen into a temporary texture using your shader, then back to the screen
        Blit(cmd, source, tempRT, settings.pixelateMaterial);
        Blit(cmd, tempRT, source);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        tempRT?.Release();
        tempRT = null;
    }
}