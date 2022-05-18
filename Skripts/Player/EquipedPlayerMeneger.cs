using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipedPlayerMeneger : MonoBehaviour
{
    public bool eqipted = false;

    private GameObject Weapon;
    private SpriteRenderer GameWeaponSprite;

    public Animator animator;
    public Sprite weapon_Sprite;

    private SpriteRenderer Arm1;
    private SpriteRenderer Arm2;
    private Sprite Arm1Sprite;
    private Sprite Arm2Sprite;
    Parameters eqip = new Parameters() {equip = false};



    private void Awake()
    {
        Weapon = transform.Find("Weapon").Find("Player_Weapon").gameObject;
        Arm1 = transform.Find("Arm1").Find("Player_arms_1").gameObject.GetComponent<SpriteRenderer>();
        Arm2 = transform.Find("Arm2").Find("Player_arms_0").gameObject.GetComponent<SpriteRenderer>();
        Arm1Sprite = Arm1.sprite;
        Arm2Sprite = Arm2.sprite;

        GameWeaponSprite = Weapon.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetWeapon();
    }
    public struct Parameters
    {
        public bool equip ;

    }
    
    public void GetWeapon()
    {
        

        if (Input.GetKeyDown(KeyCode.F) && GameWeaponSprite != null)
        {

            if (eqipted)
            {
                eqipted = false;    
                eqip.equip = false;

                animator.SetBool("equip", false);
                GameWeaponSprite.sprite = null;
                Arm1.sprite = Arm1Sprite;
                Arm2.sprite = Arm2Sprite;


                EquipWeapon(eqip);
            }
            else
            {
                eqipted = true;
                eqip.equip = true;

                Debug.Log("Eequip Weapon");

                animator.SetBool("equip", true);
                GameWeaponSprite.sprite = weapon_Sprite;
                Arm1.sprite = null;
                Arm2.sprite = null;
                

                EquipWeapon(eqip);
            }
        }

    }
    
    
    
    
    public void EquipWeapon(Parameters Parametrs)
    {
        
        if (Parametrs.equip)
        {
            GameWeaponSprite.sprite = weapon_Sprite;
        }
        else
        {
            GameWeaponSprite.sprite = null;
        }

    }
}
