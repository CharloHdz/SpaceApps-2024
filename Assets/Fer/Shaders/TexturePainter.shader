Shader "Unlit/TexturePainter"
{
    Properties {
        // Editar color en inspector
        _PainterColor ("Painter Color", Color) = (0, 0, 0, 0)
    }

    SubShader {
        // Configuraciones de culling y z-buffer.
        Cull Off 
        ZWrite Off 
        ZTest Off

        Pass {
            CGPROGRAM
            #pragma vertex vert      
            #pragma fragment frag    
            #include "UnityCG.cginc" 

            //      VARIABLES DE TEXTURA Y PARAMETROS
            sampler2D _MainTex;     // Textura principal
            float4 _MainTex_ST;     // Coordenadas de la textura principal.

            //      VARIABLES DE LA PINTURA
            float3 _PainterPosition; // Posición 
            float _Radius;          // Radio 
            float _Hardness;        // Dureza 
            float _Strength;        // Fuerza 
            float4 _PainterColor;   // Color 
            float _PrepareUV;       // Variable UV 

            struct appdata {
                float4 vertex : POSITION; 
                float2 uv : TEXCOORD0;    
            };

            struct v2f {
                float4 vertex : SV_POSITION; 
                float2 uv : TEXCOORD0;       
                float4 worldPos : TEXCOORD1; 
            };

            //      MASCARA DE PINTURA
            float mask(float3 position, float3 center, float radius, float hardness) {
                float m = distance(center, position);                // Calcula la distancia desde el centro
                return 1 - smoothstep(radius * hardness, radius, m); // Devuelve un valor de mascara suave
            }

            //      VERTICE
            v2f vert(appdata v) {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); // Convierte pos del objeto a pos mundial
                o.uv = v.uv;                                     // Mantiene las coordenadas de textura
                
                //      AJUSTE COORDENADAS UV
                float4 uv = float4(0, 0, 0, 1);
                uv.xy = float2(1, _ProjectionParams.x) * (v.uv.xy * float2(2, 2) - float2(1, 1));
                o.vertex = uv; 
                return o; 
            }

            //      FRAGMENTO
            fixed4 frag(v2f i) : SV_Target {
                // Si _PrepareUV > 0, devuelve un color azul
                if (_PrepareUV > 0) {
                    return float4(0, 0, 1, 1);
                }         

                // Obtiene el color de la textura original
                float4 col = tex2D(_MainTex, i.uv);
                // Calcula la mascara de pintura
                float f = mask(i.worldPos, _PainterPosition, _Radius, _Hardness);
                // Calcula el borde de la pintura en base a la fuerza
                float edge = f * _Strength;
                // Interpola entre el color original y el color de pintura
                return lerp(col, _PainterColor, edge);
            }
            ENDCG
        }
    }
}