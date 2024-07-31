using UnityEngine;

public class Destroying : MonoBehaviour
{
    [SerializeField] private GameObject destructablePrefab;



    public void ExplodeTheObject()
    {
        if (destructablePrefab != null)
        {
            Debug.Log("Instantiating destructible prefab.");
            GameObject destruct = Instantiate(destructablePrefab);
            destruct.transform.position = transform.position;

            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("DestructablePrefab is not assigned!");
        }
    }
}
