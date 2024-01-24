using UnityEngine;

public  class  DisableSpriteRender : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	public void OnEnable() {
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		spriteRenderer.enabled = false;
	}
}