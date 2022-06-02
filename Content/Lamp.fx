#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float2 position;
sampler  ColorSampler  : register(s0);

float4 MainPS(float2 TexCoords : TEXCOORD0) : COLOR
{
		   float d = distance(TexCoords, position) * 15;
		   float4 color = tex2D(ColorSampler, TexCoords);
		   color = lerp(color, float4(0,0,0,1), d);
	 return color;
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};