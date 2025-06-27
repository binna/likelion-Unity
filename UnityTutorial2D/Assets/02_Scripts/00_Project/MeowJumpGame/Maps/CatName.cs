using UnityEngine;

namespace Cat
{
    public class CatName : MonoBehaviour
    {
        public Transform cat;
        public Vector3 offset;

        void Update()
        {
            transform.position = cat.transform.position + offset;
        }
    }
}