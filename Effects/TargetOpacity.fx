sampler uImage0 : register(s0);

float Opacity;

float4 Alpha(float2 coords : TEXCOORD0) : COLOR0
{
    return tex2D(uImage0, coords) * Opacity;
}

technique T
{
    pass TargetOpacity
    {
        PixelShader = compile ps_2_0 Alpha();
    }
}