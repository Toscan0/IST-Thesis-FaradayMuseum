using UnityEngine;

public class MyMagnetTexture : MyScrollTexture
{
    public override float ScrollSpeedX { get => -scrollSpeed; set => scrollSpeed = -value; }
    public override float ScrollSpeedY { get => 0; set => scrollSpeedY = 0; }

    [SerializeField]
    private float scrollSpeed = 0.5f;
    public float ScrollSpeed { set { scrollSpeed = value; } }

    private float scrollSpeedY;
    
}
