using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodySleep : MonoBehaviour
{
    private int sleepCountdown = 4;
    private Rigidbody rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleepCountdown > 0)
        {
            rigid.Sleep();
            sleepCountdown--;
        }
    }
}
