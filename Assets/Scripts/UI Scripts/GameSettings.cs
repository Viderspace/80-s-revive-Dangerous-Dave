using System.Collections.Generic;
using Dave_Related;
using UnityEngine;
namespace UI_Scripts
{
    public class GameSettings : MonoBehaviour
    /* This script manages the UI input from the player, when game is launched (aka 'Startup UI').
     * DropDown menu's allows the player to set custom Keyboard-controls, and even allows the player to choose
     * what level to start the game from.
     *
     * the functions (here in the script) take care of the player's selections from the startup UI,
     * and modifying in run-time the relevant variables in the game, according to the player selections.
     */
    {
        #region Inspector
        
        [SerializeField] private GameManager gameManager;
        [SerializeField] private DaveController daveController;
        
        #endregion

        #region Fields (Dictionaries for UI DropDown-Input's)
        
        private readonly Dictionary<int, KeyCode> _jumpKeys 
            = new Dictionary<int, KeyCode>() {{0, KeyCode.UpArrow}, {1, KeyCode.Backspace}, {2, KeyCode.LeftControl}};
        
        
        private readonly Dictionary<int, KeyCode> _jetpackKeys 
            = new Dictionary<int, KeyCode>() {{0, KeyCode.LeftShift}, {1, KeyCode.X}, {2, KeyCode.LeftAlt} };
        
        
        private readonly Dictionary<int, KeyCode> _fireKeys 
            = new Dictionary<int, KeyCode>() {{0, KeyCode.Backspace}, {1, KeyCode.Z}};
        
        #endregion
        
        #region Methods

        public void SetJumpKey(int key)
        {
            Debug.Log(_jumpKeys[key]);
            daveController.jumpKey = _jumpKeys[key];
        }

        public void SetJetpackKey(int key)
        {
            Debug.Log(_jetpackKeys[key]);
            daveController.jetKey = _jetpackKeys[key];
        }

        public void SetFireKey(int key)
        {
            Debug.Log(_fireKeys[key]);
            daveController.shootKey = _fireKeys[key];
        }

        public void EnableSound(bool value)
        {
            AudioListener.volume = value ? 1f : 0f;
        }
        
        public void SelectLevel(int levelIndex)
        {
            gameManager.StartFromLevel = (GameManager.InitLevel) levelIndex + 1;
        }

        #endregion
        
        #region MonoBehaviour

        private void Start()
        {
            gameManager.StartFromLevel = (GameManager.InitLevel) 1;
        }

        #endregion
    }
}
