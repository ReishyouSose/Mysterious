sampler uImage0 : register(s0);
texture2D tex0; 
sampler2D uImage1 = sampler_state  
{
    Texture = <tex0>; 
    MinFilter = Linear; 
    MagFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

float2 offset;//偏移量
float invAlpha;//反色量
float4 PSFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float2 vec = coords;
    if (any(tex2D(uImage1, coords)))//如果传入的贴图上有颜色
    {
        vec += offset;//偏移
    }
    float4 color = tex2D(uImage0, vec);
    float4 invColor = color;
    if (any(tex2D(uImage1, coords)))
    {
        invColor.rgb = 1 - invColor.rgb; //反色
        return color * (1 - invAlpha) + invColor * invAlpha;
    }
    return color;
}
technique Technique1
{
    pass Offset
    {
        PixelShader = compile ps_2_0 PSFunction();
    }
}