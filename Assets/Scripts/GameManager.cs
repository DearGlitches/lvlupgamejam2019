using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
    public int PlayerAir = 1000;
    public float PlayerSpeed = 6f;

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
    
    
    public Image fadeOutUIImage;
    public float FadeLength = 1f; 
    public enum FadeDirection
    {
	    In, //Alpha = 1
	    Out // Alpha = 0
    }
    
    void OnEnable()
    {
	    StartCoroutine(Fade(FadeDirection.Out));
    }
    
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
		Debug.Log("CurrentLevel: " + CurrentLevel);
		Debug.Log("CurrentLevelState: " + CurrentLevelState);
		Debug.Log("CurrentGameState: " + CurrentGameState);
		switch (CurrentGameState) {
			case GameState.Start:
				if (Input.GetAxis("Vertical") < 0) {
					CurrentGameState = GameState.Game;
					_initLevel = true;
					Debug.Log("Load Scene: " + CurrentLevel);
					StartCoroutine(FadeAndLoadScene(FadeDirection.In, Levels[CurrentLevel].Name));
				}
				break;
			case GameState.End:
				SceneManager.LoadScene("End");
				if (Input.GetAxis("Vertical") > 0)
					CurrentGameState = GameState.Start;
				break;
			case GameState.Game:
				UpdateLevel ();
				break;
		}
		
	}

	private void InitLevel () {
		if (CurrentGameState != GameState.Game)
			return;
		
		InitLevelParameters(); 
	}

	private void InitLevelParameters()
	{
		CurrentLevelState = LevelState.Ready;
	}
	
	public bool CanPlay(){
		return CurrentGameState == GameState.Game && CurrentLevelState == LevelState.Play;
	}

	private void UpdateLevel()
	{
		
		Debug.Log("CurrentLevel: " + CurrentLevel);
		Debug.Log("CurrentLevelState: " + CurrentLevelState);
		Debug.Log("CurrentGameState: " + CurrentGameState);
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
				if (CurrentLevel < Levels.Length - 1)
					CurrentLevel++;
				break;
            case LevelState.Dead:
	            CurrentLevelState = LevelState.Ready;
	            _initLevel = true;
	            Debug.Log("Reload Current Scene");
                StartCoroutine(FadeAndLoadScene(FadeDirection.In, Levels[CurrentLevel].Name));
                break;

		}
	}
    
    private IEnumerator Fade(FadeDirection fadeDirection) 
    {
	    float alpha = (fadeDirection == FadeDirection.Out)? 1 : 0;
	    float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 1;
	    if (fadeDirection == FadeDirection.Out) {
		    while (alpha >= fadeEndValue)
		    {
			    SetColorImage (ref alpha, fadeDirection);
			    yield return null;
		    }
		    fadeOutUIImage.enabled = false; 
	    } else {
		    fadeOutUIImage.enabled = true; 
		    while (alpha <= fadeEndValue)
		    {
			    SetColorImage (ref alpha, fadeDirection);
			    yield return null;
		    }
	    }
    }
    
    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad) 
    {
	    yield return Fade(fadeDirection);
	    if (fadeDirection == FadeDirection.In && !sceneToLoad.Equals("NO_CHANGE"))
			SceneManager.LoadScene(sceneToLoad);
    }
    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
	    fadeOutUIImage.color = new Color (fadeOutUIImage.color.r,fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
	    alpha += Time.deltaTime * (1f/FadeLength) * ((fadeDirection == FadeDirection.Out)? -1 : 1) ;
    }
}
