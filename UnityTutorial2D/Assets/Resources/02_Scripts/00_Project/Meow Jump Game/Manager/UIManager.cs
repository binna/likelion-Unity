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
        private GameObject palyGame;
        
        [SerializeField] 
        private GameObject introUI;

        [SerializeField] 
        private GameObject alertUI;
        
        [SerializeField]
        private GameObject gameOver;
        
        [SerializeField] 
        private CatController catController;
        
        [SerializeField]
        private ColliderEvent colliderEvent;
        
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

            palyGame.SetActive(true);
            introUI.SetActive(false);
            
            nameTextUI.text = inputField.text;
            catController.CreateGame();
            colliderEvent.CreateGame();
        }

        public void OnMoveLobbyButton()
        {
            palyGame.SetActive(false);
            introUI.SetActive(true);
            gameOver.SetActive(false);
        }
    }
}