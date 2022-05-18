using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class player : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    //parameters
    private float speed = 1.5f;
    public int health = 100;

    //

    //statuss
    public bool CanMoved = true;
    public bool Death = false;
    //

    Vector3 mousePos;

    public SpriteRenderer spriteRenderer;
    public Sprite PlayerWithWeapon;
    
    public Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();   
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
    }
    private void Update()
    {


    }
    private void FixedUpdate()
    {
        
        InputProcess();
        Move();
        
    }
    public void Move()
    {
        transform.Translate(moveDelta * Time.deltaTime);
    }
    public void InputProcess()
    {
        if (CanMoved == true)
        {
            

            float x = Input.GetAxisRaw("Horizontal") * speed;
            float y = Input.GetAxisRaw("Vertical") * speed;

            animator.SetFloat("run", Mathf.Abs(x+y));
            



            moveDelta = new Vector3(x, y, 0);

            

            
        }
        
    }

}

