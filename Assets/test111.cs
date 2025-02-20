using UnityEngine;

public class test111 : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float value = 0f;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;
    public Vector3 Lerp(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value)
    {
        Vector3 a = Vector3.Lerp(p1, p2, value);
        Vector3 b = Vector3.Lerp(p2, p3, value);
        Vector3 c = Vector3.Lerp(p3, p4, value);

        Vector3 d = Vector3.Lerp(a, b, value);
        Vector3 e = Vector3.Lerp(b, c, value);

        Vector3 f = Vector3.Lerp(d, e, value);
        return f;
    }


    public void FixedUpdate()
    {
        transform.localPosition = Lerp(p1, p2, p3, p4, value);
    }




}
