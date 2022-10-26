sampler uImage0 : register(s0) ;
texture2D tex0;
sampler2D uImagel = sampler_state
{
    Texture = <tex0>;
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};
float i;
float4 PSFunction(float2 coords : TEXCOORD0): COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 color2 = tex2D(uImagel, coords);
    if (!any (color2)) return color;
    else
    {
        float2 vec= float2 (0, 0);
        float rot = color2.r * 6.28;
        vec= float2(cos(rot), sin(rot)) * color2.g * i;
        return tex2D(uImage0, coords + vec);
    }
}
float4 PSFunction2(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 color2 = tex2D(uImagel, coords);
    if (!any(color2))
        return color;
    else
    {
        float2 vec = float2(0, 0);
        float rot = color2.g * 6.28;
        vec = float2(cos(rot), sin(rot)) * color2.r * i;
        return tex2D(uImage0, coords + vec);
    }
}

technique Technique1
{
	pass Mag
    {
		PixelShader = compile ps_2_0 PSFunction();
	}
    pass Mag2
    {
        PixelShader = compile ps_2_0 PSFunction2();
    }
}
