using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))] //이거 구동할때 알아서 추가

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

    public bool ground = false;

    private bool isClimbing = false;

    Transform startTr;
    Vector3 startVector;
    public GameObject setdowncamera;

    bool isSitDown = false;



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
    }
    private void Update()
    {

        //뒤돌기
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("누름");
            yRotation += 180;
            if (yRotation >= 360)
            {
                yRotation -= 360;
            }
            transform.rotation = Quaternion.Euler(0, yRotation, 0);

        }
        

        //반동처럼
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(RotateSmoothly(-1f,0.1f));
        }
        Rotate();



        ground = Physics.Raycast(transform.position, Vector3.down, 0.5f);

        if (ground == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    


    void FixedUpdate()
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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 힘을 가해 점프
    }

    

    void Move(float slowspeed)
    {
        h = Input.GetAxisRaw("Horizontal"); // 수평 이동 입력 값
        v = Input.GetAxisRaw("Vertical");   // 수직 이동 입력 값

        // 입력에 따라 이동 방향 벡터 계산
        Vector3 moveVec = transform.forward * v*0.5f + transform.right * h;

        // 이동 벡터를 정규화하여 이동 속도와 시간 간격을 곱한 후 현재 위치에 더함
        transform.position += moveVec.normalized * moveSpeed * slowspeed* Time.deltaTime;
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed*0.01f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * 0.01f;

        yRotation += mouseX;    
        xRotation -= mouseY;   

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 수직 회전 값을 -90도에서 90도 사이로 제한

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // 카메라의 회전을 조절
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // 플레이어 캐릭터의 회전을 조절


    }



}
