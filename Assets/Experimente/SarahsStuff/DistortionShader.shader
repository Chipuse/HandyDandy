Shader "Unlit/DistortionShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        [Normal] _DistortionTex ("Distortion Texture", 2D) = "bump" {}

        _distortionStrenght ("Distortion Strenght", float) = 0.5  
        
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        GrabPass
        {
            "_GrabTexture"
        }
        
        Pass
        {
            ZWrite Off
            Blend  SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 DisUV : TEXCOORD1;
            };


            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.DisUV = ComputeGrabScreenPos(o.vertex);
                o.uv = v.uv;
                return o;
            }
            
            sampler2D _MainTex;
            sampler2D _GrabTexture;
            sampler2D _DistortionTex;
            
            float _distortionStrenght;
                   
            fixed4 frag (v2f i) : SV_Target
            {

                
                fixed main = tex2D(_MainTex, i.uv).x;
                float2 dis = UnpackNormal (tex2D(_DistortionTex, i.uv)).xy;
                dis *= _distortionStrenght *main;
                i.DisUV.xy +=dis*i.DisUV.z;
                fixed4 col = tex2Dproj(_GrabTexture, i.DisUV);

                return col;
            }
            ENDCG
        }
    }
}
