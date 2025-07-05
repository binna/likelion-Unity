using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject inventoryUI;
        
        [SerializeField]
        private Button inventoryButton;
        
        [SerializeField] 
        private GameObject[] items;

        [SerializeField] 
        private Slot[] slots;
        
        [SerializeField] 
        private Transform slotGroup;

        void Start()
        {
            // 자신과 자식 중에서 Slot Component가 있는 대상을 모두 가져오는 기능
            slots = slotGroup.GetComponentsInChildren<Slot>(true);
            
            // 인벤토리 버튼을 눌렀을 때, OnInventory 함수 실행
            inventoryButton.onClick.AddListener(OnClickInventory);
        }

        public void OnClickInventory()
        {
            inventoryUI.SetActive(true);
        }

        public void DripItem(Vector3 dropPosition)
        {
            // 랜덤 아이템 설정
            var randomIdx = Random.Range(0, items.Length);
            
            // 아이템 생성
            var item = Instantiate(items[randomIdx], dropPosition, Quaternion.identity);
            var itemRigidbody2D = item.GetComponent<Rigidbody2D>();

            // 랜덤한 방향으로 힘을 가하는 기능
            dropPosition = new Vector3(
                dropPosition.x +  Random.Range(-2f, 2f), 
                dropPosition.y + 3f, 
                dropPosition.z); 
            itemRigidbody2D.AddForce(dropPosition, ForceMode2D.Impulse);
            
            // 랜덤한 회전으로 힘을 가하는 기능
            float ranPower = Random.Range(-1.5f, 1.5f);
            itemRigidbody2D.AddTorque(ranPower, ForceMode2D.Impulse);
        }

        public void GetItem(Item item)
        {
            // 인벤토리에 넣는 기술
            foreach (var slot in slots)
            {
                if (slot.isEmpty)
                {
                    slot.AddItem(item);
                    break;
                }
            }
        }
    }
}