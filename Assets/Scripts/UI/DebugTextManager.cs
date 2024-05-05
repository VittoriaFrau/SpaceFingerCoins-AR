namespace UI
{
    using UnityEngine;
    using TMPro;

    public class DebugTextManager : MonoBehaviour
    {
        public static DebugTextManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI debugText;

        private void Awake()
        {
            if (!debugText.gameObject.activeSelf) return;
            // Ensure only one instance of DebugTextManager exists
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Duplicate instance of DebugTextManager. Destroying...");
                Destroy(gameObject);
            }

            // Ensure the GameObject persists across scenes
            DontDestroyOnLoad(gameObject);
        }

        public void SetDebugText(string text)
        {
            if (!debugText.gameObject.activeSelf) return;
            //if the new text is different from the current one, set the new text
            if (debugText.text != text)
            {
                debugText.text = text;
            }
            //add a new line to the debug text
            debugText.text +=  "\n"+ text;
        }

        public void AddDebugText(string text)
        {
            if (!debugText.gameObject.activeSelf) return;
            //add a new line to the debug text
            debugText.text +=  "\n"+ text;
        }
    }

}