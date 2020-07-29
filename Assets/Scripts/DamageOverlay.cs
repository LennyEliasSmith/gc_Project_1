using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    public Image overlayImage;

    private float r;
    private float g;
    private float b;
    private float a;

    private void Start()
    {
        r = overlayImage.color.r;
        g = overlayImage.color.g;
        b = overlayImage.color.b;
        a = overlayImage.color.a;
    }

    private void AdjustColor()
    {
        Color c = new Color(r, g, b, a);
        overlayImage.color = c;
    }
    
}
