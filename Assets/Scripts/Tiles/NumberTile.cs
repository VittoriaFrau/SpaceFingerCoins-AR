using System.Collections;
using TMPro;
using UnityEngine;

public class NumberTile : MonoBehaviour
{
    private bool isGrabbed = false;
    private TextMeshProUGUI text;
    private Vector3 initialPosition, initialRotation;
    private TableCollisionHandler tableCollisionHandler;
    [SerializeField] GameObject tilePrefab;
    private Coroutine fadeCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
        text = GetComponentInChildren<TextMeshProUGUI>();
        tableCollisionHandler = GameObject.FindGameObjectWithTag("Desk").GetComponent<TableCollisionHandler>();
        text.text = "?";
        
    }
    
    private IEnumerator FadeOutAndDestroy()
    {
        // Fai scomparire gradualmente la tile
        float duration = 3f; // Durata dell'animazione di fade-out
        float timer = 0f;
        Color initialColor = text.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        // Se la tile non è stata afferrata, distruggila
        if (!isGrabbed)
        {
            Destroy(gameObject);
            // Crea una nuova tile con il testo '?'
            SpawnNewTile();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(text.text==null) return;
        if (other.gameObject.CompareTag("finger") && text.text!="?")
        {
            SetIsGrabbed(true);
            //After 1.5 seconds, spawn a new tile
            Invoke("SpawnNewTile", 1.5f);
            tableCollisionHandler.StopCountingFingers();
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("finger"))
        {
            SetIsGrabbed(false);
            tableCollisionHandler.StopCountingFingers();
        }
    }
    
    private void SpawnNewTile()
    {
        //Check if in that position there is already a tile
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f))
        {
            if (hit.collider.CompareTag("NumberTile"))
            {
                return;
            }
        }
        GameObject newTile= Instantiate(tilePrefab, initialPosition, Quaternion.identity);
        newTile.transform.eulerAngles = initialRotation;
        newTile.GetComponentInChildren<TextMeshProUGUI>().text = "?";
        TilesManager.Instance.SetCurrentActiveTile(newTile);
    }
    
    public void SetIsGrabbed(bool grabbed)
    {
        isGrabbed = grabbed;
        // Interrompi l'animazione di fade-out se la tile viene afferrata
        if (grabbed && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            // Reimposta il testo a '?' se la tile viene afferrata durante l'animazione
            text.color = Color.white;
            text.text = "?";
        }
    }
    
    public void StartFadeOut()
    {
        fadeCoroutine = StartCoroutine(FadeOutAndDestroy());
    }
}
