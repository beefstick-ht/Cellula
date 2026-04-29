Shader "Custom/Pixelate"
{
    Properties
    {
        // URP Blit uses _BlitPassSource instead of _MainTex
        [HideInInspector] _BlitPassSource ("Source Texture", 2D) = "white" {}
        _BlockSize ("Block Size", Float) = 4
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Use the URP standard naming
            sampler2D _BlitPassSource;
            float _BlockSize;
            float4 _BlitPassSource_TexelSize;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings i) : SV_Target
            {
                // Calculate pixelation based on screen resolution
                float2 blockSize = _BlockSize * _BlitPassSource_TexelSize.xy;
                float2 pixelUV = floor(i.uv / blockSize) * blockSize + (blockSize * 0.5);
                return tex2D(_BlitPassSource, pixelUV);
            }
            ENDHLSL
        }
    }
}