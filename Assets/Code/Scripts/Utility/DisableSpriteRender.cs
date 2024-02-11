using UnityEngine;

/// <summary>
/// Class for disable sprite renderer
/// </summary>
public class DisableSpriteRender : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	public void OnEnable() {
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		spriteRenderer.enabled = false;
	}
}