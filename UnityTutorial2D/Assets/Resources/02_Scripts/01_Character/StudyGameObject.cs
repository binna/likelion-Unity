using UnityEngine;

public class StudyGameObject : MonoBehaviour
{
    public GameObject prefab;
    
    // Start 함수보다 먼저 실행되는 Awake 함수
    void Awake()
    {
        CreateAmongus();
    }

    public void CreateAmongus()
    {
        GameObject obj = Instantiate(prefab);
        obj.name = "어몽어스 캐릭터";
    }
}