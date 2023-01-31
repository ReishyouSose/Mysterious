sampler uImage0 : register(s0);
float2 uImageSize;

float strenth; //1到0之间
//float r;//发光半径，至少为1
float3 lightcolor; //rgb

float4 PSFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords); //图片取样
    float dx = 1 / uImageSize.x; //每个像素的纹理大小
    float dy = 1 / uImageSize.y; //每个像素的纹理大小
    if (any(color))
        return color;
    float4 result = float4(lightcolor, strenth);
    float l = 1;
    float x, y;
    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            float4 c = tex2D(uImage0, coords + float2(dx * i, dy * j));
            // 如果任何一个像素有颜色
            if (any(c))
            {
                x = i;
                y = j;
                break;
            }
        }
    }
    l = sqrt(x * x + y * 2);
    return float4(lightcolor, lerp(strenth, 0, l));
}
technique Technique1
{
    pass move
    {
        PixelShader = compile ps_3_0 PSFunction();
    }
}