using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp_Paintable : MonoBehaviour
{
    const int texture_Size = 1024;
    public float extendIslandOffset = 1;

    RenderTexture extendIslandTexture;
    RenderTexture uvIslandTexture;
    RenderTexture maskTexture;
    RenderTexture supportTexture;

    Renderer rend;

    int maskTextureID = Shader.PropertyToID("_MaskTexture");

    public RenderTexture getExtend() => extendIslandTexture;
    public RenderTexture getUVIslands() => uvIslandTexture;
    public RenderTexture getMask() => maskTexture;
    public RenderTexture getSupport() => supportTexture;
    public Renderer getRenderer() => rend;

    private void Start()
    {
        extendIslandTexture = new RenderTexture(texture_Size, texture_Size, 0);
        extendIslandTexture.filterMode = FilterMode.Bilinear;

        uvIslandTexture = new RenderTexture(texture_Size, texture_Size, 0);
        uvIslandTexture.filterMode = FilterMode.Bilinear;

        maskTexture = new RenderTexture(texture_Size, texture_Size, 0);
        maskTexture.filterMode = FilterMode.Bilinear;

        supportTexture = new RenderTexture(texture_Size, texture_Size, 0);
        supportTexture.filterMode = FilterMode.Bilinear;

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandTexture);

        PaintManager.instance.initTextures(this);
    }

    private void OnDisable()
    {
        extendIslandTexture.Release();
        uvIslandTexture.Release();
        maskTexture.Release();
        supportTexture.Release();
    }
}
