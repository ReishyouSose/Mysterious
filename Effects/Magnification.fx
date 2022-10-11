sampler uImage0 : register(s0) ;
texture2D tex0;
sampler2D uImagel = sampler_state//对texO的纹理采样
{
    Texture = <tex0>;
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};
float i;
float4 PSFunction(float2 coords : TEXCOORDO): COLORO//用一张图片〈uImagel)去扭曲uImage0，r代表方向，g代表大小
{
    float4 color = tex2D(uImage0, coords);
    float4 color2= tex2D(uImagel, coords);
    if (!any (color2)) return color;
    else
    {
        float2 vec= float2 (0, 0);//表示移动的向量
        float rot = color2.r *6.28;//把r(0~1)转化为弧度制的角度(0~2*pi)vec= float2(cos(rot)，sin(rot))*color2.g*i;
        return tex2D(uImage0,coords + vec);
    }
}
