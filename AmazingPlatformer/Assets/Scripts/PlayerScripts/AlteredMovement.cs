using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredMovement : MonoBehaviour
{
    public Collider2D targetCollider;
    public Vector2 closestPoint;

    //assign the layer to forbidden where all the taget objects are 
    [SerializeField]
    private LayerMask forbidden;

    private Rigidbody2D myBody;

    //for head-On collision assign directioOfMovement at Run-Time 
    private Vector2 directionOfMovement;
    
    [SerializeField]
    private float detectionRange = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bounce1();
        Bounce2();
    }
    /*
    void Bounce1()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, detectionRange, forbidden))
            myBody.AddForce(Vector2.down, ForceMode2D.Impulse);
        if (Physics2D.Raycast(transform.position, Vector2.down, detectionRange, forbidden))
            myBody.AddForce(Vector2.up, ForceMode2D.Impulse);
        if (Physics2D.Raycast(transform.position, Vector2.right, detectionRange, forbidden))
            myBody.AddForce(Vector2.left, ForceMode2D.Impulse);
        if (Physics2D.Raycast(transform.position, Vector2.left, detectionRange, forbidden))
            myBody.AddForce(Vector2.right, ForceMode2D.Impulse);

        //for head-on collision and bounce back
        //if (Physics2D.Raycast(transform.position, directionOfMovement, detectionRange, forbidden))
        //    myBody.AddForce(-directionOfMovement, ForceMode2D.Impulse);
    }
    */
    void Bounce2()
    {
        Vector2 point = targetCollider.ClosestPoint(transform.position);
        closestPoint=point;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Forbidden")
            myBody.AddForce(-closestPoint * 2f, ForceMode2D.Impulse);
    }
}
