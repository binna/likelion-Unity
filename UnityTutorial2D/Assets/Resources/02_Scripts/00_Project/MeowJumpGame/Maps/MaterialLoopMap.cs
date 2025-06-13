using UnityEngine;

namespace Cat
{
    public class MaterialLoopMap : MonoBehaviour
    {
        [SerializeField]
        private float offsetSpeed = 0.1f;

        private MeshRenderer _meshRenderer;
        
        void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();

        }

        void Update()
        {
            Vector2 offset = Vector2.right * offsetSpeed * Time.deltaTime;

            _meshRenderer.material.SetTextureOffset("_MainTex", _meshRenderer.material.mainTextureOffset + offset);
        }
    }
}