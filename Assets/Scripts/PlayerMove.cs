using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))] //�̰� �����Ҷ� �˾Ƽ� �߰�

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    public float mouseSpeed;
    float yRotation;
    float xRotation;
    Camera cam;

    public float moveSpeed;
    float h;
    float v;
    public float jumpForce;

    bool isground = false;

    private bool isClimbing = false;

    Transform startTr;
    Vector3 startVector;
    public GameObject setdowncamera;

    bool isSitDown = false;
    Collider coll;
    public bool isFreeView = false;



    void Start()
    {

        rb = GetComponent<Rigidbody>();            

        cam = Camera.main;                          
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startTr = GetComponent<Transform>();
        startTr.position = transform.position;

        //setdowncamera = GetComponent<GameObject>();
        startVector = setdowncamera.transform.localPosition;
        coll = GetComponent<Collider>();
    }
    private void Update()
    {
        //�ڵ���
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("����");
            yRotation += 180;
            if (yRotation >= 360)
            {
                yRotation -= 360;
            }
            transform.rotation = Quaternion.Euler(0, yRotation, 0);

        }

        //�ݵ�ó��//ȭ�� �ö�
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(RotateSmoothly(-1f,0.1f));
        }

        //���콺ȸ��//������������
        if (isFreeView == false)
        {
            Rotate();
        }


        //����
        isground = Physics.Raycast(transform.position, Vector3.down, 0.5f);
        if (isground == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //�ӽ� ���ӸŴ������� �ϸ� �ɵ�
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            if (isFreeView == false)
            {
                isFreeView = true;
            }
            else if(isFreeView == true)
            {
                PlayerSettingReset();
                isFreeView = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Debug.Log("dsds");
            transform.position = new Vector3(5, -11, -5);
        }


    }
    


    void FixedUpdate()
    {
        //��ٸ� ����
        //Climbing();
        

        //�ɱ�
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSitDown = true;
            setDown();
        }
        else if (Input.GetKeyUp((KeyCode.LeftControl)))
        {
            isSitDown = false;
            float time1 = 0;
            while (time1 < 1f)
            {
                time1 += Time.deltaTime;
                setdowncamera.transform.localPosition = Vector3.Lerp(setdowncamera.transform.localPosition, startVector, time1 / 1f);
            }

        }

        //�̵� ����
        //��������
        if (isFreeView == true)
        {
            FreeMove();
        }
        else if (isFreeView == false)
        {
            //õõ������, �׳ɰ���
            if (Input.GetKey(KeyCode.LeftShift) || isSitDown == true)
            {
                Move(0.5f);
            }
            else
            {
                Move(1f);

            }
        }


    }

    //����
    private void PlayerSettingReset()
    {
        rb.useGravity = true;
        coll.enabled = true;
        
    }
    

    //�ݵ�ó�� ���̱�
    IEnumerator RotateSmoothly(float rotationAmount, float duration)
    {
        float startRotation = xRotation;
        float targetRotation = xRotation + rotationAmount;
        float time6 = 0f;
        yRotation += Random.Range(-0.5f, 0.5f);

        while (time6 < duration)
        {
            time6 += Time.deltaTime;
            xRotation = Mathf.Lerp(startRotation, targetRotation, time6 / duration);
            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            yield return null; 
        }

        xRotation = targetRotation; 
    }
    
    //��ٸ� ����
    void Climbing()
    {
        if (isClimbing)
        {
            transform.position += new Vector3(0, v * 0.01f, 0);
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            isClimbing = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            isClimbing = false;
        }
    }
    void setDown()
    {
        Vector3 ds = setdowncamera.transform.localPosition;
        ds.y -= 0.3f;

        setdowncamera.transform.localPosition = ds;
    }

    void Jump()
    {
        rb.velocity = Vector3.zero;
        Physics.gravity = new Vector3(0, -40.0F, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ���� ���� ����
    }

    //�������� //Rotate(),move()���ߴ�
    void FreeMove()
    {
        rb.useGravity = false;
        coll.enabled = false;

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * 0.01f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * 0.01f;

        yRotation += mouseX;
        xRotation -= mouseY;


        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);

        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        transform.position += move * 5 * Time.deltaTime;

    }
    

    void Move(float slowspeed)
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = transform.forward * v * 0.5f + transform.right * h;
        transform.position += moveVec * moveSpeed * slowspeed * Time.deltaTime;

    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed*0.01f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * 0.01f;

        yRotation += mouseX;    
        xRotation -= mouseY;   

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���� ȸ�� ���� -90������ 90�� ���̷� ����

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // ī�޶��� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // �÷��̾� ĳ������ ȸ���� ����


    }



}
