using Cinemachine;
using UnityEngine;

namespace Main
{
    public class Teleport : MonoBehaviour
    {
        public GameObject portal;
        private GameObject player;
        private Rigidbody2D playerRigidbody;
        //[SerializeField] private CinemachineVirtualCamera camera;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Vector3 teleportPosition = portal.transform.position;

                Vector3 prevPosition = player.transform.position;

                if (playerRigidbody.velocity.x > 0) 
                {
                    teleportPosition.x += 1; 
                }
                else if (playerRigidbody.velocity.x < 0) 
                {
                    teleportPosition.x -= 1; 
                }

                player.transform.position = teleportPosition;

                //camera.OnTargetObjectWarped(player.transform, teleportPosition - prevPosition);
            }
        }
    }
}
