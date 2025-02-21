using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("ºÕ ¸¶¿ì½ºUpÇÔ");
            ThrowGrenade();
        }
        CheckGrounded();
        //gravite2();
    }
    Vector3 throwDirection;
    void ThrowGrenade()
    {
        transform.parent = null;
        //velocity.y = gravite * Time.deltaTime;
        //transform.position += velocity;
        rb.constraints = (RigidbodyConstraints)0;
        throwDirection = Camera.main.transform.forward;
        rb.AddForce(throwDirection *1300);
        rb.useGravity = true;
        //StartCoroutine(boomWait());
    }
    
   

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
    }
    IEnumerator boomWait()
    {

        yield return new WaitForSeconds(3f);

        //Destroy(gameObject);
    }

}
