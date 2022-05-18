using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{

    private Transform transformArm1;
    private Transform transformArm2;
    private Transform transformWeapon;
    private Transform transformPlayer;
    private bool orientetionEarlier—hanged;
    //private bool orientetionChekChenged;
    private float angle1;
    private float angle2;
    private float angleWeapon;
    private Vector3 mousePos;
    private Transform FirePointTransform;
    
    public Texture2D Aim;

    public void Start()
    {
        AimMouse();
    }
    private void Awake()
    {
        transformArm1 = transform.Find("Arm1");
        transformArm2 = transform.Find("Arm2");
        transformWeapon = transform.Find("Weapon");
        FirePointTransform = transformWeapon.Find("FirePoint");
        transformPlayer = gameObject.transform;
        orientetionEarlier—hanged = false;
        
        
    }

    void Update()
    {
        //have bug with cheng orientetion when mouse on player

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AimMouse();

        ArmMovement();

        WeaponAim();



        //Debug.Log(orientetionEarlier—hanged);
        //Debug.Log(transformPlayer.localScale.x);

        //Debug.Log(angle1);
        //Debug.Log(orientetionChekChenged);
        //Debug.Log(FirePointTransform);
    }

    private void ArmMovement()
    {
        Vector3 aimDirectionArm1 = (mousePos - transformArm1.position).normalized;
        Vector3 aimDirectionArm2 = (mousePos - transformArm2.position).normalized;
        if (transformPlayer.localScale.x < 0)
        {

            angle1 = Vector2.SignedAngle(Vector2.left, aimDirectionArm1);
            angle2 = Vector2.SignedAngle(Vector2.left, aimDirectionArm2);

            if (angle1 < 75f && angle1 > -75f)
                orientetionEarlier—hanged = false;

            if (angle1 > 90f || angle1 < -90f)
            {
                if (orientetionEarlier—hanged == false)
                {
                    orientetionEarlier—hanged = true;
                    transformPlayer.localScale = Vector3.one;
                    //FirePointTransform.Rotate(0f,180f,0f);
                }
                
            }
            

        }
        else
        {

            angle1 = Vector2.SignedAngle(Vector2.right, aimDirectionArm1);
            angle2 = Vector2.SignedAngle(Vector2.right, aimDirectionArm2);

            if (angle1 < 75f && angle1 > -75f)
                orientetionEarlier—hanged = false;

            if (angle1 > 90f || angle1 < -90f)
            {
                if (orientetionEarlier—hanged == false)
                {
                    orientetionEarlier—hanged = true;
                    transformPlayer.localScale = new Vector3(-1,1,1);
                    //FirePointTransform.Rotate(0f, 180f, 0f);
                }
                
            }
            
        }
       
        transformArm1.eulerAngles = new Vector3(0, 0, angle1);
        transformArm2.eulerAngles = new Vector3(0, 0, angle2);
    }
    private void WeaponAim()
    {
        
        if (transformWeapon != null)
        {
            Vector3 aimDirectionWeapon = (mousePos - transformWeapon.position).normalized;

            if (transformPlayer.localScale.x < 0)
            {
                angleWeapon = Vector2.SignedAngle(Vector2.left, aimDirectionWeapon);
            }
            else
            {
                angleWeapon = Vector2.SignedAngle(Vector2.right, aimDirectionWeapon);
            }

            transformWeapon.eulerAngles = new Vector3(0, 0, angleWeapon);
            
        }
        
    
    }
    public void AimMouse()
    {

        Cursor.SetCursor(Aim, Vector2.zero, CursorMode.Auto);
    }
}
