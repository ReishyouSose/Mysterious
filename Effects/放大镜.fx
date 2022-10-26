sampler uImage0 : register(s0);
sampler uImage1 : register(s1);

float2 fix;
float2 pos;
float mult;
bool followdis;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 c2 = tex2D(uImage1, coords);
    if (!any(c2))
        return color;
    else
    {
    // pos 就是中心了
    //float2 pos = float2(0.5, 0.5);
    // offset 是中心到当前点的向量
        float2 offset = (coords - pos);
    // 因为长宽比不同进行修正
        float2 rpos = offset * fix;
        float dis = length(rpos);
    // 向量长度缩短0.8倍
        if(followdis)
            return tex2D(uImage0, coords + offset * dis * mult);
        else
        {
            return tex2D(uImage0, coords + offset * mult);
        }
    }
}

technique Technique1
{
    pass enlarge
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
