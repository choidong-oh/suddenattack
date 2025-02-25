using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boom : MonoBehaviour, IDamageable
{
    Rigidbody rb;
    Vector3 velocity;
    bool isGrounded = false;
    public GameObject explodePrefab; //Æø¹ß ÇÁ¸®ÆÕ
    float radius = 5f;//Æø¹ß ¹üÀ§
    public LayerMask toExplode;
    int boomDagage = 1000;

    //¿Àµð¿À
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ShootStart;
    [SerializeField] private AudioClip ShootEnd;

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

    void OnEnable()
    {
        gameObject.SetActive(true   );
    }

    Vector3 throwDirection;
    void ThrowGrenade()
    {
        audioSource.PlayOneShot(ShootStart);
        transform.parent = null;
        //velocity.y = gravite * Time.deltaTime;
        //transform.position += velocity;
        rb.constraints = (RigidbodyConstraints)0;
        throwDirection = Camera.main.transform.forward;
        rb.AddForce(throwDirection *1300);
        //rb.AddForce(throwDirection *10);
        rb.useGravity = true;
        StartCoroutine(boomWait());
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
        Explode();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }



    void Explode()
    {
        audioSource.PlayOneShot(ShootEnd);
        //Instantiate(explodePrefab, transform.position, Quaternion.identity);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,3,Vector3.up,0f,LayerMask.GetMask("Enemy"));
        GameObject fish = Instantiate(explodePrefab);
        fish.transform.SetParent(transform, false);
        //Instantiate(explodePrefab,transform.position, Quaternion.identity); 
        foreach (var hitobj in hits)
        {
            var dsd = hitobj.collider.GetComponent<IDamageable>();
            dsd.GetDamge(boomDagage);
            Debug.Log("°¨Áö‰Î");


        }


    }

    public void GetDamge(int damage)
    {




    }
}
