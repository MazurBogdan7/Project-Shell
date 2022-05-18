using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressed : Collideble
{
    protected bool pressed;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
            OnPressed();
    }
    protected virtual void OnPressed()
    {
        pressed = true;
    }

}
