sampler uIamge0 : register(s0);

float2 center;
float2 mult;// 扭曲率
float r_in;
float r_out;
float fix;// 长宽比

float4 SpaceWrap(float2 coords : TEXCOORD0) : COLOR0
{
    float2 offset = coords - center;
    float dis = length(offset * fix);
    //if (r_in / 2 < dis && dis < r_out / 2)
    {
        return tex2D(uIamge0, center + offset * dis * mult);
    }
    return tex2D(uIamge0, coords);
}

technique Technique1
{
    pass Wrap
    {
        PixelShader = compile ps_2_0 SpaceWrap();
    }
}