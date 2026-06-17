using UnityEngine;

public class SphereGizmoExample : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private Color color = new Color(1f, 0.2f, 0.2f, .5f);

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}