using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile proj))
        {
            Goal.goalMet = true;

            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 0.75f;
            mat.color = c;
        }
    }
}
