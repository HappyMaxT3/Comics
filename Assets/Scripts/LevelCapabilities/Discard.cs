using UnityEngine;

namespace Main
{
    public class Discard : MonoBehaviour
    {
        [SerializeField] private float discardDistance = 1.0f;

        public void DiscardObject(Vector3 direction)
        {
            transform.position += direction * discardDistance;
        }
    }
}
