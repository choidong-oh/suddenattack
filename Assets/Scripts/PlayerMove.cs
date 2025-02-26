using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))] //이거 구동할때 알아서 추가

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

    //오디오
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
        //사다리 구현
        Climbing();
        

        //앉기
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

        //이동 구현
        //자유시점
        if (isFreeView == true)
        {
            FreeMove();
        }
        else if (isFreeView == false)
        {
            //천천히가기, 그냥가기
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



    //뒤돌기
    private void playerTurnBack()
    {
        yRotation += 180;
        if (yRotation >= 360)
        {
            yRotation -= 360;
        }
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    //리셋
    private void PlayerSettingReset()
    {
        rb.useGravity = true;
        coll.enabled = true;
        
    }
    
    //반동처럼
    private void RotateUP()
    {
        StartCoroutine(RotateSmoothly(-1f, 0.1f));
    }

    //반동처럼 보이기
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
    
    //사다리 구현
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
        //점프
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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 힘을 가해 점프
    }

    //자유시점 모드
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


    //자유시점 //Rotate(),move()꺼야댐
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

    //마우스 스피드 변경
    void MouseSpeed(float speed)
    {
        Debug.Log("마우스");
        mouseSpeed += speed;
    }

    void Move(float slowspeed)
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = transform.forward * v * 0.5f + transform.right * h;
        transform.position += moveVec * moveSpeed * slowspeed * Time.deltaTime;

    }

    //자유시점 모드 아닌
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
