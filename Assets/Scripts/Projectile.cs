using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    const int LOOKBACK_COUNT = 10;
    static List<Projectile> PROJECTILES = new();

    [SerializeField]
    private bool _isAwake = true;

    private Vector3 prevPos;
    private List<float> deltas = new();
    private Rigidbody rigid;


    public bool IsAwake
    {
        get { return _isAwake; }
        private set { _isAwake = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        IsAwake = true;
        prevPos = new Vector3(1000, 1000, 0);
        deltas.Add(1000);
        PROJECTILES.Add(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rigid.isKinematic || !IsAwake) return;

        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add(deltaV3.magnitude);
        prevPos = transform.position;

        while (deltas.Count > LOOKBACK_COUNT) {
            deltas.RemoveAt(0);
        }

        float maxDelta = 0;
        foreach (float delta in deltas)
        {
            maxDelta = Mathf.Max(maxDelta, delta);
        }

        if (maxDelta <= Physics.sleepThreshold)
        {
            IsAwake = false;
            rigid.Sleep();
        }
    }

    private void OnDestroy()
    {
        PROJECTILES.Remove(this);
    }

    public static void DESTROY_PROJECTILES()
    {
        foreach (Projectile proj in PROJECTILES)
        {
            Destroy(proj.gameObject);
        }
    }
}
