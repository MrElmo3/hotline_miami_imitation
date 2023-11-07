using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void OnEnable() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

}
