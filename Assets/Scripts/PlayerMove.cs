using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))] //�̰� �����Ҷ� �˾Ƽ� �߰�

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    public float mouseSpeed = 3f;
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

    //�����
    public AudioSource audioSource;
    public AudioClip JumpAudio;

    [SerializeField] private GameManager gameManager;


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

        gameManager.OnTurnBack += playerTurnBack;
        gameManager.OnMouseSpeed += MouseSpeed;
        gameManager.OnRotateUp += RotateUP;
        gameManager.OnMouseRotate += NoFreeView;
        gameManager.OnFreeView += IsFreeMove;
        gameManager.OnHome += StartResetPoition;
        gameManager.OnJump += Isjump;

    }
   


    void FixedUpdate()
    {
        //��ٸ� ����
        Climbing();
        

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

    void StartResetPoition()
    {
        transform.position = new Vector3(5, -11, -5);
    }



    //�ڵ���
    private void playerTurnBack()
    {
        yRotation += 180;
        if (yRotation >= 360)
        {
            yRotation -= 360;
        }
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    //����
    private void PlayerSettingReset()
    {
        rb.useGravity = true;
        coll.enabled = true;
        
    }
    
    //�ݵ�ó��
    private void RotateUP()
    {
        StartCoroutine(RotateSmoothly(-1f, 0.1f));
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
        var leg = transform.position;
        leg.y -= 0.3f;

        Ray ray = new Ray(leg,transform.forward);
        RaycastHit hit;
        int isStairs = LayerMask.GetMask("Stairs");
        if(Physics.Raycast(ray,out hit,0.2f, isStairs))
        {
            transform.position += new Vector3(0, v * 0.01f, 0);
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
       

    }
    
    void setDown()
    {
        Vector3 ds = setdowncamera.transform.localPosition;
        ds.y -= 0.3f;

        setdowncamera.transform.localPosition = ds;
    }

    void Isjump()
    { 
        //����
        isground = Physics.Raycast(transform.position, Vector3.down, 0.5f);
        if (isground == true )
        {
            Jump();
        }

    }
    void Jump()
    {
        audioSource.PlayOneShot(JumpAudio);
        rb.velocity = Vector3.zero;
        Physics.gravity = new Vector3(0, -40.0F, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ���� ���� ����
    }

    //�������� ���
    void IsFreeMove()
    {

        if (isFreeView == false)
        {
            isFreeView = true;
        }
        else if (isFreeView == true)
        {
            PlayerSettingReset();
            isFreeView = false;
        }

    }


    //�������� //Rotate(),move()���ߴ�
    void FreeMove()
    {
        rb.useGravity = false;
        coll.enabled = false;

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * 0.1f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * 0.1f;

        yRotation += mouseX;
        xRotation -= mouseY;


        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);

        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        transform.position += move * 5 * Time.deltaTime;

    }

    //���콺 ���ǵ� ����
    void MouseSpeed(float speed)
    {
        Debug.Log("���콺");
        mouseSpeed += speed;
    }

    void Move(float slowspeed)
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = transform.forward * v * 0.5f + transform.right * h;
        transform.position += moveVec * moveSpeed * slowspeed * Time.deltaTime;

    }

    //�������� ��� �ƴ�
    void NoFreeView()
    {
        if (isFreeView == false)
        {
            Rotate();
        }

    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed*0.1f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed *0.1f;

        yRotation += mouseX;    
        xRotation -= mouseY;   

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); 


    }



}
