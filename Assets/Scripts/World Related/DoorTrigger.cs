using UnityEngine;
using UnityEngine.SceneManagement;

namespace World_Related
{
    public class DoorTrigger : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private LevelData levelData;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Camera mainCam;

        #endregion

        #region Properties

        private bool IsLoaded { get; set; }

        #endregion


        #region Methods

        public void Locked(bool key)
            /* when the player collects the key, the door collider is set to trigger-mode (the player can "enter" it),
         but up until then, the door behaves like a regular "hard-wall" collider.*/
        {
            GetComponent<Collider2D>().isTrigger = key;
        }


        private void MoveCamToNewScene()
        {
            mainCam.transform.position = levelData.camInitPos;
        }


        private void LoadNextLevel()
        {
            if (IsLoaded) return;
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            IsLoaded = true;
        }


        private void UnLoadCurrentLevel()
        {
            Debug.Log(levelData.prevLevelName);
            SceneManager.UnloadSceneAsync(levelData.prevLevelName);
        }

        #endregion


        #region MonoBehaviour

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            mainCam = FindObjectOfType<Camera>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Dave") || !gameManager.HasKey) return;

            if (levelData.endGame)
            {
                Debug.Log("Game Completed! Thank you for playing");
                GameManager.QuitGame();
                return;
            }

            Debug.Log($"Entered {gameObject.name}");
            LoadNextLevel();
            MoveCamToNewScene();
            UnLoadCurrentLevel();
            gameManager.NextLevel();
        
        }

        #endregion
    }
}