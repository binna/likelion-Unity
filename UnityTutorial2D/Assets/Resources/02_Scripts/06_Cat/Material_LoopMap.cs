using UnityEngine;

public class Material_LoopMap : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    
    public float offsetSpeed = 0.1f;
    
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
