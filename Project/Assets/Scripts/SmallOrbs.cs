using UnityEngine;

public class SmallOrbs : MonoBehaviour
{
    public float shrinkSpeed = 2f; 
    public float minScale = 0.5f;
    public float maxScale = 1f; 

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * shrinkSpeed, 1f));
        transform.localScale = originalScale * scale;
    }
}
