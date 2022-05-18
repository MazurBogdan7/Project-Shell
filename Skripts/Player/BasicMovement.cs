using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    //This movment all persons in game

    public void Move(Vector3 movePerson,float speed)
    {
        transform.Translate(movePerson * Time.deltaTime * speed);
    }
    public void InputProcess(Vector3 movePerson)
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movePerson = new Vector3(x, y, 0);

        if (movePerson.x > 0)
            transform.localScale = Vector3.one;
        else if (movePerson.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);


    }
}
