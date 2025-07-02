using UnityEngine;

namespace Knight
{
    public class RadderEvent : MonoBehaviour
    {
        public enum Type
        {
            Center,
            Surface
        }

        [SerializeField]
        private Type type;

        [SerializeField]
        private GameObject radderLeft;
        
        [SerializeField]
        private GameObject radderRight;

        private void OnTriggerStay2D(Collider2D other)
        {
            // if (other.CompareTag("Player"))
            // {
            //     switch (type)
            //     {
            //         case Type.Center:
            //             radderLeft.SetActive(true);
            //             radderRight.SetActive(true);
            //             
            //             
            //             if (other
            //                     .gameObject
            //                     .GetComponent<KnightController_Keyboard>()
            //                     .GetHorizontal() != 0f)
            //             {
            //                 radderLeft.SetActive(false);
            //                 radderRight.SetActive(false);
            //             }
            //             break;
            //         case Type.Surface:
            //             radderLeft.SetActive(false);
            //             radderRight.SetActive(false);
            //             break;
            //     }
            //
            //     Debug.Log(type + ", " + other.name);
            //}
        }
    }
}