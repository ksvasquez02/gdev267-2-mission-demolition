using UnityEngine;

public class CloudCover : MonoBehaviour
{
    [Header("Inscribed")]
    public Sprite[] cloudSprites;
    public int numClouds = 40;
    public Vector3 minPos = new Vector3(-20, -5, -5);
    public Vector3 maxPos = new Vector3(300, 40, 5);
    [Tooltip("x is the min value, and y is the max value")]
    public Vector2 scaleRange = new Vector2(1, 4);
    [Tooltip("x is the min value, and y is the max value")]
    public float cloudSpeed = 0.5f;
    public float depthMulti = 0.1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject cloudGO;
        SpriteRenderer sRend;
        float scaleMulti;
        for (int i = 0; i < numClouds; i++)
        {
            cloudGO = new GameObject();
            sRend = cloudGO.AddComponent<SpriteRenderer>();

            int spriteNum = Random.Range(0, cloudSprites.Length);
            sRend.sprite = cloudSprites[spriteNum];

            cloudGO.transform.position = RandomPos();
            cloudGO.transform.SetParent(transform, true);

            scaleMulti = Random.Range(scaleRange.x, scaleRange.y);
            cloudGO.transform.localScale = scaleMulti * Vector3.one;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numClouds; i++)
        {
            Transform cloudTrans = transform.GetChild(i);
            Vector3 cloudPos = cloudTrans.localPosition;

            float depth = (1 + maxPos.z - cloudPos.z) * depthMulti;
            cloudPos.x += Time.deltaTime * depth * cloudSpeed;

            cloudTrans.localPosition = cloudPos;
        }
    }

    private Vector3 RandomPos()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(minPos.x, maxPos.x);
        pos.y = Random.Range(minPos.y, maxPos.y);
        pos.z = Random.Range(minPos.z, maxPos.z);
        return pos;
    }
}
