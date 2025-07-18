using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coinCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement.coinCount++;
            
            Debug.Log($"현재까지 {Movement.coinCount}개의 코인 획득!!");
            
            Destroy(gameObject);
        }
    }
}