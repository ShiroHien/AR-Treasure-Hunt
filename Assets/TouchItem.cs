using UnityEngine;
using UnityEngine.UI;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

namespace ARTreasureHunt
{
    public class ItemClickedScript : MonoBehaviour
    {
        private GameManager gameManager;
        public GameObject ItemIconObject; // GameObject containing the Image component
        private Image itemIcon; // Reference to the Image component
        public GameObject ConfettiAnimationPrefab;
        private string imageTarget; // String to store the image target name

        private void Start()
        {
            gameManager = FindAnyObjectByType<GameManager>();
            if (ItemIconObject != null)
            {
                itemIcon = ItemIconObject.GetComponent<Image>();
            }
        }

        private void OnEnable()
        {
            EnhancedTouch.TouchSimulation.Enable();
            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += OnFingerTouch;
        }

        private void OnDisable()
        {
            EnhancedTouch.TouchSimulation.Disable();
            EnhancedTouch.EnhancedTouchSupport.Disable();
            EnhancedTouch.Touch.onFingerDown -= OnFingerTouch;
        }

        private void Update()
        {
            // Check for mouse input (for testing in the Editor)
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouchOrClick(Input.mousePosition);
            }
        }

        private void OnFingerTouch(EnhancedTouch.Finger finger)
        {
            if (finger.index != 0) return;
            HandleTouchOrClick(finger.currentTouch.screenPosition);
        }

        private void HandleTouchOrClick(Vector2 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ItemFound();
                }
            }
        }

        public void ItemFound()
        {
            gameManager.HiddenItemFound();

            // Update the single icon if it's available
            if (itemIcon != null)
            {
                itemIcon.color = new Color(255f, 255f, 255, 255f);
            }

            gameObject.GetComponent<Collider>().enabled = false;
            var animatedConfetti = Instantiate(ConfettiAnimationPrefab, transform);
            animatedConfetti.SetActive(true);
            Invoke("RemoveItem", 3);
        }

        private void RemoveItem()
        {
            Destroy(gameObject);
        }

        public void SetImageTarget(string target)
        {
            imageTarget = target;
        }
    }
}
