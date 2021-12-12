using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject dave;

    public bool IsLoaded { get; private set; }
    private Vector2 _level2CamPos;
    private readonly int panCamRightAmount = 10;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        mainCam = FindObjectOfType<Camera>();
        dave = FindObjectOfType<DaveController>().gameObject;
        
        _level2CamPos = mainCam.transform.position + Vector3.right * panCamRightAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Dave") || !gameManager.HasKey) return;
        
        Debug.Log($"Entered {gameObject.name}");
        LoadScene();
        MoveToNewScene();
        gameManager.NextLevel();
        UnLoadPrevScene();


    }

    private void MoveToNewScene()
    {
        mainCam.transform.position = levelData.camInitPos;
        dave.transform.position = levelData.daveInitPos;
        
    }


    private void LoadScene()
    {
        if (IsLoaded) return;
        
        SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
        IsLoaded = true;
    }

    private void UnLoadPrevScene()
    {
        SceneManager.UnloadSceneAsync(levelData.prevLevelName);
    }
    
}
