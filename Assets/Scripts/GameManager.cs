using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum DevelopmentState
    {
        Development,
        Production
    }

    public enum GameState
    {
        Start,
        Game,
        End
    }

    public enum LevelState
    {
        Start,
        Ready,
        End,
        Play,
        Dead
    }

    [Header("Player Settings")]
    public int PlayerAir = 2;
    public float PlayerSpeed = 2f;

    [Header("Game Settings")]
    public Level[] Levels = new Level[0];

    public int CurrentLevel;
    public DevelopmentState CurrentDevState = DevelopmentState.Development;
    public GameState CurrentGameState = GameState.Game;

    private static GameManager _gameManager;
    public LevelState CurrentLevelState = LevelState.Ready;
    private Vector3 _playerStartPosition;
    private bool _finishLevel;
    private bool _initLevel;
    private bool _resetLevel;

    private GameObject _startPanel;
    private GameObject _uiPanel;
    private GameObject _diePanel;
    private GameObject _player;
    
    // Use this for initialization
	void Awake () {
		if (_gameManager == null)
		{
			_gameManager = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		
		//just for playing the scene during debug
		if (CurrentGameState == GameState.Game) {
			CurrentLevel = 0;
			_initLevel = true;
		}

        _resetLevel = false;
	}

	
	// Update is called once per frame
	void Update () {
		switch (CurrentGameState) {
			case GameState.Start:
			case GameState.End:
			case GameState.Game:
				UpdateLevel ();
				break;
		}
		
	}

	private void InitLevel () {
		if (CurrentGameState != GameState.Game)
			return;
		_player = GameObject.Find ("Claudine");
		if (_player != null)
		    _playerStartPosition = _player.transform.position;
		else
			Debug.LogError ("Object with 'Claudine' name not found");
		
		InitLevelParameters(); 
		
	}

	private void InitLevelParameters()
	{
		CurrentLevelState = LevelState.Ready;
		if (_player != null) {
			_player.transform.position = _playerStartPosition;
		    _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}
	}
	
	public bool CanPlay(){
		return CurrentGameState == GameState.Game && CurrentLevelState == LevelState.Play;
	}

	private void UpdateLevel()
	{
		if (_initLevel)
		{
			InitLevel();
			_initLevel = false;
		}

		switch (CurrentLevelState)
		{
			case LevelState.Ready:
				CurrentLevelState = LevelState.Start;
				break;
			case LevelState.Start:
				CurrentLevelState = LevelState.Play;
				break;
			case LevelState.Play:
				break;
			case LevelState.End:
				CurrentLevelState = LevelState.Ready;
				_initLevel = true;
				break;
            case LevelState.Dead:
                StartCoroutine(WaitForSceneLoad());
                break;

		}
	}

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
