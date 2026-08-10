using UnityEngine;

public class UpgradeText : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destoryTime = 1f;

    
    void Start()
    {

        Destroy(gameObject, destoryTime);
    }

    
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
