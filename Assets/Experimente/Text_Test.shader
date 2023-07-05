
Shader "ShaderCourse/Test01"
{
	//UI of the Shader
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_GlyphTex("Glyphs", 2D) = "white" {}
		_GlyphSizeX("Glyph Size Width", Int) = 8
		_GlyphSizeY("Glyph Size Height", Int) = 16
		_SymbolCount("Symbol Rows Count", Int) = 128
		_Test("test", float) = 0
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" }
			LOD 100

			//GrabPass
			//{
				//"_BackgroundTexture"
			//}

			Pass
			{
				CGPROGRAM
				#pragma vertex VertexShader_
				#pragma fragment FragmentShader

				#include "UnityCG.cginc"

				struct VertexData
				{
					float4 position : POSITION;
					float3 normal   : NORMAL;
					float2 uv       : TEXCOORD0;
				};

				struct VertexToFragment
				{
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
					float4 screenPosition : VECTOR;
					//float4 grabPos : TEXCOORD1;
				};

				float inverseLerp(float v, float min, float max)
				{
					return (v - min) / (max - min);
				}

				float remap(float v, float min, float max, float outMin, float outMax)
				{
					float t = inverseLerp(v, min, max);
					return lerp(outMin, outMax, t);
				}

				sampler2D _MainTex;
				//sampler2D _BackgroundTexture;
				sampler2D _GlyphTex;
				float _GlyphSizeX;
				float _GlyphSizeY;
				float _SymbolCount;
				float _Test;

				VertexToFragment VertexShader_(VertexData vertexData)
				{
					VertexToFragment output;
					output.position = UnityObjectToClipPos(vertexData.position);
					output.uv = vertexData.uv;
					output.screenPosition = ComputeScreenPos(output.position);

					//output.grabPos = ComputeGrabScreenPos(output.position);

					return output;
				}

				// GPU IS DOING THINGS WITH THE DATA

				float4 FragmentShader(VertexToFragment vertexToFragment) : SV_Target
				{
					// sample the texture
					//float2 newUv = vertexToFragment.uv - (_GlyphSizeX * floor(vertexToFragment.uv / _GlyphSizeX));
					//float v1 = vertexToFragment.uv.x - (_GlyphSizeX * floor(vertexToFragment.uv.x / _GlyphSizeX));
					//float v2 = vertexToFragment.uv.y - (_GlyphSizeX  * floor(vertexToFragment.uv.y / _GlyphSizeX));

					//create 
					//float2 screenPosition = (vertexToFragment.screenPosition.x / vertexToFragment.screenPosition.w, vertexToFragment.screenPosition.y /= vertexToFragment.screenPosition.w);
					float2 screenUv = (vertexToFragment.screenPosition);

					//Pixelate:
					float2 gridPixels = float2(_ScreenParams.x / _GlyphSizeX, _ScreenParams.y / _GlyphSizeY);
					float2 gridScreenPos = gridPixels * vertexToFragment.screenPosition;
					float2 gridFraction = frac(gridScreenPos);

					float2 pixelation = floor(gridScreenPos);
					pixelation /= gridPixels;


					//half4 bgcolor = tex2Dproj(_BackgroundTexture, vertexToFragment.grabPos);

					float4 mainTextPixelated = tex2D(_MainTex, pixelation);

					//GreyScale
					float greyscales = (mainTextPixelated.x + mainTextPixelated.y + mainTextPixelated.z) / 3;

					// Horizontal displacement
					float2 glyphRange = float2(0, _SymbolCount - 1);

					float greyscaleMappedToGRange = remap(greyscales, 0, 1, glyphRange.x, glyphRange.y);
					greyscaleMappedToGRange = floor(greyscaleMappedToGRange);
					greyscaleMappedToGRange = remap(greyscaleMappedToGRange, glyphRange.x, glyphRange.y, 0, 1);
					if (_Test != 0) {
						greyscaleMappedToGRange = _Test;
					}
					float greyscaleStep = greyscaleMappedToGRange * remap(_GlyphSizeX / _SymbolCount, 0, 1, glyphRange.x, glyphRange.y);

					//Grid cell UVs:
					gridFraction.x = (gridFraction.x / _SymbolCount) + greyscaleStep;


					float4 col = tex2D(_GlyphTex, gridFraction);
					return col;
				}
				ENDCG
			}
		}
}
