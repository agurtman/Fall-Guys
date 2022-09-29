using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] float limit = 75f;

    void Update()
    {
        float angle = limit * Mathf.Sin(Time.time);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
