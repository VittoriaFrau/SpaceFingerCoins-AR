using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneChoice;
    private static float lastLoadTime = 0f;
    public float cooldown = 1.0f; 

    // Start is called before the first frame update
    void Start()
    {
        if (sceneChoice == null)
        {
            sceneChoice = this.name;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other.gameObject.name: " + other.gameObject.transform.parent);
        if (other.gameObject.transform.parent.name.Contains("Point"))
        {
            
            if (Time.time - lastLoadTime < cooldown)
            {
                Debug.Log("Cooldown active.");
                return;
            }

            Debug.Log("Finger entered the trigger");
            lastLoadTime = Time.time;
            //Load the next scene
            LoadScene();
        }
    }

    public void LoadScene()
    {
        switch (sceneChoice)
        {
            case "MoneyScene":
                // load the Money scene
                SceneManager.LoadScene("Money");
                break;
            case "CartesianScene":
                SceneManager.LoadScene("Cartesian");
                break;
            case "FingersScene":
                SceneManager.LoadScene("AppleFingers");
                break;
            default:
                Debug.Log("No scene loaded");
                break;
        }
    }
}
