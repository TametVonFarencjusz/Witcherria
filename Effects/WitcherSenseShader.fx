sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4 WitcherSenseShader(float2 coords : TEXCOORD0) : COLOR0
{
 	float4 color = tex2D(uImage0, coords);
	float2 coords2;
	coords2.x = coords.x - 0.5f;
	coords2.y = coords.y - 0.5f;

	float2 coords3;
	coords3.x = coords2.x * uScreenResolution.x;
	coords3.y = coords2.y * uScreenResolution.y;
	
	float wav = sin(uTime * 2.0f);

	float power = 15.0f + wav * 1.25f;
	float powerFinal = (uIntensity) * 2.0f;
	float middle = 25.0f;

	float dist = sqrt(coords3.x * coords3.x + coords3.y * coords3.y);
	if(dist > middle)
	{
		float value = 1.0f - sqrt(dist) / power * powerFinal;
		float grey = (color.r + color.g + color.b) / 3.0f;
		
		color.r = color.r * value + grey * (1.0f - value);
		color.g = color.g * value + grey * (1.0f - value);
		color.b = color.b * value + grey * (1.0f - value);
	}
	else
	{
		float value = 1.0f - sqrt(middle) / power * powerFinal;
		float grey = (color.r + color.g + color.b) / 3.0f;
		
		color.r = color.r * value + grey * (1.0f - value);
		color.g = color.g * value + grey * (1.0f - value);
		color.b = color.b * value + grey * (1.0f - value);
	}
	return color;
}

technique Technique1
{
    pass WitcherSenseShader
    {
        PixelShader = compile ps_2_0 WitcherSenseShader();
    }
}