Shader "Unlit/ExtendIsland"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}             // Textura principal
        _UVIslands ("Texture UVIslands", 2D) = "white" {} // UV Map
        _OffsetUV ("UVOffset", float) = 1                 // Desplazamiento UV para calcular pixeles cercanos
    }

    SubShader {
        Tags { "RenderType"="Opaque" } 
        LOD 100 

        Pass {
            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag 
            #pragma target 3.0 

            #include "UnityCG.cginc" 

            struct appdata {
                float4 vertex : POSITION;   // Posicion del vertice
                float2 uv : TEXCOORD0;      // Coordenadas UV
            };

            struct v2f {
                float2 uv : TEXCOORD0;       // Coordenadas UV transformadas
                float4 vertex : SV_POSITION; // Posicion del vertice en espacio
            };

            //      VARIABLES DE TEXTURA Y PARAMETROS
            sampler2D _MainTex; // Textura 
            float4 _MainTex_ST; // Coordenadas 
            float4 _MainTex_TexelSize; // Tamaño 
            float _OffsetUV; // Desplazamiento UV
            sampler2D _UVIslands; // Textura que define las islas UV

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Convierte la Posicion del objeto a espacio de clip
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // Transforma las coordenadas UV segun la textura principal
                return o; // Retorna los datos de salida del vertice
            }

            fixed4 frag (v2f i) : SV_Target {
                //      DESPLAZAMIENTO PIXELES CERCANOS
                float2 offsets[8] = {
                    float2(-_OffsetUV, 0),          // Left
                    float2(_OffsetUV, 0),           // Right
                    float2(0, _OffsetUV),           // Up
                    float2(0, -_OffsetUV),          // Down
                    float2(-_OffsetUV, _OffsetUV),  // Up Left
                    float2(_OffsetUV, _OffsetUV),   // Up Right
                    float2(_OffsetUV, -_OffsetUV),  // Down Right
                    float2(-_OffsetUV, -_OffsetUV)  // Down Left
                };

                float2 uv = i.uv;                           // Coordenadas UV actuales
                float4 color = tex2D(_MainTex, uv);         // Obtiene el color de la textura principal
                float4 island = tex2D(_UVIslands, uv);      // Obtiene información sobre la isla en las coordenadas UV

                // Verifica si el pixel  pertenece a una isla
                if (island.z < 1) {
                    float4 extendedColor = color; 

                    // Recorre los desplazamientos y extiende el color
                    for (int j = 0; j < 8; j++) {
                        float2 currentUV = uv + offsets[j] * _MainTex_TexelSize.xy;     // Calcula las coordenadas UV desplazadas
                        float4 offsettedColor = tex2D(_MainTex, currentUV);             // Obtiene el color del pixel desplazado
                        extendedColor = max(offsettedColor, extendedColor);             // Extiende el color tomando el maximo
                    }
                    color = extendedColor;                                              // Actualiza el color final con el color extendido
                }

                return color;                                                           // Regresa el color final del fragmento
            }
            ENDCG 
        }
    }
}