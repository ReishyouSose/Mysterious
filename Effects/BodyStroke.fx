sampler tex : register(s0);
sampler screen : register(s1);

float2 Size;
float4 color;
float lerp = -1;

float4 BodyStroke(float2 coords :TEXCOORD0) : COLOR0
{
    float4 w = tex2D(tex, coords);
    if (any(w))
        return w;
    
        // 获取每个像素的正确大小
    float dx = 1 / Size.x;
    float dy = 1 / Size.y;
        // 对周围8格进行判定
    for (int i = -1; i <= 1; i++)
    {
        float4 c = tex2D(tex, coords + float2(dx * i, 0));
                // 如果任何一个像素有颜色
        if (any(c))
        {
            return color * (lerp == -1 ? 1 : lerp);
        }
    }
    for (int j = -1; j <= 1; j++)
    {
        float4 c = tex2D(tex, coords + float2(0, dy * j));
                // 如果任何一个像素有颜色
        if (any(c))
        {
            return color * (lerp == -1 ? 1 : lerp);
        }
    }
    return tex2D(screen, coords);
}

technique T1
{
    pass BS
    {
        PixelShader = compile ps_2_0 BodyStroke();
    }
}