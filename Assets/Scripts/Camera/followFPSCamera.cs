using UnityEngine;
public class followFPSCamera : MonoBehaviour
{
    public Transform target;

    void update()
    {
        transform.position = target.position;
    }
}