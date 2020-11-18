using UnityEngine;

public abstract class ScrollTexture : MonoBehaviour
{   
    public abstract float ScrollSpeedX { get; set;}
    public abstract float ScrollSpeedY {get; set;}

    void Update()
    {
        float offsetX = Time.time * ScrollSpeedX;
        float offsetY = Time.time * ScrollSpeedY;

        GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, 1);
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(-offsetX,-offsetY);
    }

    public void InvertTexture()
    {
        ScrollSpeedX *= -1;
        ScrollSpeedY *= -1;
    }

    public void InvertTextureX()
    {
        ScrollSpeedX *= -1;
    }

    public void InvertTextureY()
    {
        ScrollSpeedY *= -1;
    }
}
