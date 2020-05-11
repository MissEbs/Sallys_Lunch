using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target = null; //what the camera is following
    [SerializeField] float smoothing = 0; //how fast it is moving
    [SerializeField] Vector2 maxPosition = Vector2.zero; //x and y
    [SerializeField] Vector2 minPosition = Vector2.zero;

    void LateUpdate()
    {
        if (transform.position != target.position) //if position is not at target position...
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing); //find distance from targe and move a bit towards
        }
    }
}
