using UnityEngine;

public class movingBackground : MonoBehaviour
{
    public Transform cameraTransform;
void LateUpdate()
    {
        transform.position = new Vector3(
            cameraTransform.position.x,
            cameraTransform.position.y,
            cameraTransform.position.z
        );
    }
}
