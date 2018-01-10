Shader "Custom/Coord" {
    Properties {
    }
    SubShader {
        Tags { "RenderType"="Transparent" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
 
        struct Input {
            float3 objPos;
        };
 
        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.objPos = v.vertex;
        }
 
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = IN.objPos;
            o.Alpha = 0.5;
        }
        ENDCG
    }
}