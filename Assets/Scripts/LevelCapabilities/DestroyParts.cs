using UnityEngine;

public class DestroyParts : MonoBehaviour
{
    [SerializeField] private Vector2 forceDir;
    [SerializeField] private float spin; 
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(forceDir, ForceMode2D.Impulse); 
            rb.AddTorque(spin, ForceMode2D.Impulse); 
        }
    }
}
