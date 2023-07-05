Shader "Unlit/BorderShader"
{
    Properties
    {
        [HDR]
        _MainColor ("Color", color) = (1, 1, 1, 1)
        
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex1 ("Noise Texture1", 2D) = "white" {}
        _NoiseTex2 ("Noise Texture2", 2D) = "white" {}
        
        _speed1 ("ScrollSpeed1", float) = 1.0
        _speed2 ("ScrollSpeed2", float) = 0.5
        _speed3 ("ScrollSpeed2", float) = 0.5
        _distortionStrenght ("Distortion Strenght", float) = 0.5
        
    }
    
    
   
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        
   
        Pass
        {
            ZWrite Off
            Blend  OneMinusDstColor One
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #define PI 3.14159

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION; 
                float2 uv2 : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex1;
            sampler2D _NoiseTex2;
                        
            float4 _MainTex_ST;
            float4 _NoiseTex1_ST;
            
            fixed4 _MainColor;
            
            float _speed1;
            float _speed2;
            float _speed3;
            float _distortionStrenght;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
               
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv, _NoiseTex1);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               
               
                float2 NoiseUV1 = i.uv2;
                float2 NoiseUV2 = i.uv2 + float2(_Time.x*_speed1, _Time.x*_speed2);
                
                fixed4 Noise1 = tex2D(_NoiseTex1, NoiseUV1);
                fixed4 Noise2 = tex2D(_NoiseTex2, NoiseUV2);
                float2 NoiseMix = (Noise1.rb + Noise2.rb)*_distortionStrenght;
                
                float2 LineUV = i.uv + float2(NoiseMix.x, NoiseMix.y+_Time.x*_speed3);
                
                fixed4 LineColor = tex2D(_MainTex, LineUV)*_MainColor;
               
                return fixed4(LineColor.rgb, LineColor.r);
            }
            ENDCG
        }
    }
}
