using UnityEngine;

[ExecuteInEditMode]
public class BoxBlur : MonoBehaviour
{
    public Material BlurMaterial;
    private int _iterations;
    private int _downRes;

    public void Blur(int iterations, int downRes)
    {
        _iterations = iterations;
        _downRes = downRes;
    }

    public void Blur()
    {
        _iterations = 10;
        _downRes = 1;
    }

    public void ResetBlur()
    {
        _iterations = 0;
        _downRes = 0;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        int width = src.width >> _downRes;
        int height = src.height >> _downRes;

        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(src, rt);

        for (int i = 0; i < _iterations; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(rt, rt2, BlurMaterial);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }

        Graphics.Blit(rt, dst);
        RenderTexture.ReleaseTemporary(rt);
    }
}