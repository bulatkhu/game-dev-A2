using UnityEngine;

public class BackgroundBrightnessControl : MonoBehaviour
{
    public SpriteRenderer background;
    public float brightnessStep = 0.1f;

    private void Update()
    {
        if (background == null) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeBrightness(-brightnessStep);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeBrightness(brightnessStep);
        }
    }

    private void ChangeBrightness(float change)
    {
        float h, s, v;
        Color.RGBToHSV(background.color, out h, out s, out v);
        v = Mathf.Clamp(v + change, 0, 1);
        background.color = Color.HSVToRGB(h, s, v);
    }
}
