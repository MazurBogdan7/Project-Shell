using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Pressed
{
    protected override void OnPressed()
    {
        pressed = true;
        Debug.Log("Portal");
    }

}
