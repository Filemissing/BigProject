using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        Vector3 toCamera = Camera.main.transform.position - transform.position;
        Vector3 target = transform.position - toCamera;
        transform.LookAt(target);
    }
}
