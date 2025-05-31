using UnityEngine;

namespace Resources._02_Scripts._06_Cat
{
    public class ColliderEvent : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Game Over");
            }
        }
    }
}