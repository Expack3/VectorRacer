float4x4 World;
float4x4 View;
float4x4 Projection;
Texture xTexture;

// TODO: add effect parameters here.
sampler ColoredTextureSampler = sampler_state
{
    texture = <xTexture>;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 texCoord : TEXCOORD0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 texCoord : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0; //initialize to 0

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	//output.Position.x = output.Position.x - (intensity * 20.0f);
	//output.Position.y += mul(sin(time * 0.005), 10);

	output.texCoord = input.texCoord;

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input, float2 pos : VPOS) : COLOR0
{
    // TODO: add your pixel shader code here.
	float4 color = tex2D(ColoredTextureSampler, input.texCoord); //grabs color of pixel at the texture coordinate

	if(color.g < 0.3)
	{
		color.a = 0; color.r = 0; color.g = 0; color.b = 0;
	}

    return color;
}

technique Brighten
{
    pass Pass1
    {
        // TODO: set renderstates here.
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
