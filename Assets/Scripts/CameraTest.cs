using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField] private Player player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);        
    }
}
