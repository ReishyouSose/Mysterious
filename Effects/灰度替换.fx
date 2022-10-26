sampler tex : register(s0);
sampler target : register(s1);
sampler origin : register(s2);

float4 GrayReplace(float2 coords : TEXCOORD0): COLOR0
{
    float4 c = tex2D(tex, coords);
    float4 o = tex2D(origin, coords);
    if (!any(c))
    {
        return o;
    }
    else
    {
        float gray = sqrt(dot(float3(0.3, 0.59, 0.11), c.rgb));
        return o * (1 - gray) + tex2D(target, coords) * gray;
    }
}

technique T1
{
    pass gr
    {
        PixelShader = compile ps_2_0 GrayReplace();
    }
}