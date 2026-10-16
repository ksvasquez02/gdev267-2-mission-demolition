using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static private FollowCam S;
    static public GameObject POI;

    public enum eView { none, slingshot, castle, both };

    [Header("Inscribed")]
    public float easing = 0.05f;
    public Vector2 minXy = Vector2.zero;
    public GameObject viewBothGO;

    [Header("Dynamic")]
    public float camZ;
    public eView nextView = eView.slingshot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        S = this;
        camZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 destination = Vector3.zero;

        if (POI != null)
        {
            Rigidbody poiRB = POI.GetComponent<Rigidbody>();

            if (poiRB != null && poiRB.IsSleeping())
            {
                POI = null;
            }
        }

        if (POI != null)
        {
            destination = POI.transform.position;
        }

        destination.x = Mathf.Max(minXy.x, destination.x);
        destination.y = Mathf.Max(minXy.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = camZ;

        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }

    public void SwitchView(eView newView)
    {
        if (newView == eView.none)
        {
            newView = nextView;
        }

        switch (newView)
        {
            case eView.slingshot:
                POI = null;
                nextView = eView.castle;
                break;
            case eView.castle:
                POI = MissionDemolition.GET_CASTLE();
                nextView = eView.both;
                break;
            case eView.both:
                POI = viewBothGO;
                nextView = eView.slingshot;
                break;

        }
    }

    public void SwitchView()
    {
        SwitchView(eView.none);
    }

    public static void SWITCH_VIEW(eView newView)
    {
        S.SwitchView(newView);
    }
}
