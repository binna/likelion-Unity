using UnityEngine;

public class Pinball : MonoBehaviour
{
    public PinballManager pinballManager;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!int.TryParse(other.gameObject.name, out int score))
            return;

        pinballManager.totalScore += score;
        
        Debug.Log($"{score}점 획득, 현재 점수 : {pinballManager.totalScore}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("GameOverZone"))
        {
            Debug.Log($"게임 종료 : 현재 점수 {pinballManager.totalScore}");
        }
    }
}