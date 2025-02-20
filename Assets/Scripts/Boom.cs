using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    Rigidbody rb;
    public GameObject playpo;
    float gravite = -9.81f;
    Vector3 velocity;
    bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        gravite = Physics.gravity.y;
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
    void ThrowGrenade()
    {
        transform.parent = null;
        //velocity.y = gravite * Time.deltaTime;
        //transform.position += velocity;
        rb.constraints = (RigidbodyConstraints)0;
        Vector3 throwDirection = Camera.main.transform.forward;
        rb.AddForce(throwDirection*30, ForceMode.Impulse);
        rb.useGravity = true;
        //StartCoroutine(boomWait());
    }
    void gravite2()
    {
        if (!isGrounded)
        {
            velocity.y += gravite * Time.deltaTime;
        }
        transform.position += velocity * Time.deltaTime;
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
        yield return new WaitForSeconds(1f);

        var ds = rb.velocity;
        ds.y = -1f;
        rb.velocity = ds;



        //rb.velocity = new Vector3(0, gravite*1.5f * Time.deltaTime, 0);
        Debug.Log(rb.velocity);


        yield return new WaitForSeconds(3f);
        //Destroy(gameObject);
    }

}
