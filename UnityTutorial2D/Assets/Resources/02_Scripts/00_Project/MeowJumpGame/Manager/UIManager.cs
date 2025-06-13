using TMPro;
using UnityEngine;

namespace Cat
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField;
        
        [SerializeField]
        private TextMeshProUGUI nameTextUI;
        
        [SerializeField]
        private TextMeshProUGUI recordTextUI;

        [SerializeField] 
        private GameObject playGame;
        
        [SerializeField] 
        private GameObject introUI;

        [SerializeField] 
        private GameObject alertUI;
        
        [SerializeField]
        private GameObject outerUI;

        [SerializeField]
        private GameObject playUI;

        [SerializeField] 
        private CatController catController;
        
        [SerializeField]
        private GameManager gameManager;
        
        void Awake()
        {
            playGame.SetActive(false);
            playUI.SetActive(false);
            introUI.SetActive(true);
            outerUI.SetActive(false);
        }
        
        public void OnAlertConfirmButton()
        {
            alertUI.SetActive(false);
        }

        public void OnStartButton()
        {
            if (inputField.text == "")
            {
                alertUI.SetActive(true);
                return;
            }

            playGame.SetActive(true);
            playUI.SetActive(true);
            introUI.SetActive(false);
            outerUI.SetActive(false);
            
            gameManager.ScoreReset();

            nameTextUI.text = inputField.text;
            catController.CreateGame();
        }

        public void OnMoveLobbyButton()
        {
            playGame.SetActive(false);
            playUI.SetActive(false);
            introUI.SetActive(true);
            outerUI.SetActive(false);
        }

        public void OuterUI(string text)
        {
            recordTextUI.text = text;
            playGame.SetActive(false);
            playUI.SetActive(false);
            introUI.SetActive(false);
            outerUI.SetActive(true);
        }
    }
}