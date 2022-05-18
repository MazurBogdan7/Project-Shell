using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    //delays
    public float boundX = 0.45f; 
    public float boundY = 0.15f;

/*
  fixes the camera on the player with a specific delay
 */
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else 
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, deltaY, 0);
    }

}
