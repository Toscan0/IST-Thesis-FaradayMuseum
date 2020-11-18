using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilsManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] pss;

    private float minI = 0.1f;
    private float maxI = 3;
    private float minR = 0.7f;
    private float maxR = 1.7f;

    private void Awake()
    {
        ManageInput.OnIntesityChanged += UpdateSize;
    }

    public void UpdateSize(float intensity)
    {
        //particleSystemSizeModule.sizeMultiplier = intensity;
        for (int i = 0; i < pss.Length; i++)
        {
            var mainModule = pss[i].main;
            if(intensity == 0)
            {
                mainModule.startSize = 0;
            }
            else
            {
                //converting the value to our scale
                mainModule.startSize = NumberConvert(intensity);
            }
        }
       
    }

    private float NumberConvert(float i)
    {
        float size = (((i - minI) * (maxR - minR)) / (maxI - minI)) + minR;

        return size;
    }

    private void OnDestroy()
    {
        ManageInput.OnIntesityChanged -= UpdateSize;
    }
}