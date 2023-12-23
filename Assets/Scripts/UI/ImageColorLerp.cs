using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageColorLerp : MonoBehaviour
{
    Image image;

    [Range(0, 1)] public float lerpTime;
    [SerializeField] private Color[] color;
    private int colorIndex = 0;
    private float time;
    void Start()
    {
        image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        image.color = Color.Lerp(image.color, color[colorIndex], lerpTime);

        time = Mathf.Lerp(time, 1f, lerpTime);
        if (time > 0.9f)
        {
            time = 0;
            colorIndex++;
            colorIndex = (colorIndex >= color.Length) ? 0 : colorIndex;
        }
    }
}
