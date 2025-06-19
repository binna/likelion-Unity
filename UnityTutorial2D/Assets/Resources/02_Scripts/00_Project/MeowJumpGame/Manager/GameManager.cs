using TMPro;
using UnityEngine;

namespace Cat
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI playTimeUI;

        [SerializeField] 
        private TextMeshProUGUI scoreUI;

        private static float _timer;
        private static int _score;

        void Update()
        {
            _timer += Time.deltaTime;

            playTimeUI.text = $"Play Time : {_timer:F1}";
            scoreUI.text = $"X {_score}";
        }

        public static void PlusScore()
        {
            _score++;
        }

        public void ScoreReset()
        {
            _score = 0;
            _timer = 0f;
        }

        public static string GetRecordText()
        {
            return $"Play Time : {_timer:F1} Sec\nChuru Given : {_score}";
        }
    }
}