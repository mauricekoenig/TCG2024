
using Fusion;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public NetworkSession Session;
    public LobbySettings LobbySettings;
    public INetworkRunnerCallbacks Callbacks;
    public GameObject NetworkSessionPrefab;
    public GameObject GameLogicManagerPrefab;
    public static NetworkManager Instance;

    public UnityEvent<GameLogicManager> OnGameLogicManagerCreated;

    private void Awake() {

        DontDestroyOnLoad(gameObject);

        if (Instance == null) {

            Instance = this;
            Callbacks = GetComponent<NetworkEvents>();
        }

        else Destroy(gameObject);
    }

    // NetworkRunner
    public void StartGame () {

        if (!Session.Runner.IsServer) return;
        Session.Runner.LoadScene(LobbySettings.GameSceneName);
    }
    public async void OpenSession () {

        if (Session != null) return;
        Session = Instantiate(NetworkSessionPrefab).GetComponent<NetworkSession>();
        await Session.Open(GameMode.AutoHostOrClient);
    }
    public void CloseSession() {

        if (!Session.Runner.IsRunning) return;
        Session.Close();
    }
    
    // Network Callbacks

    public void OnPlayerJoined (NetworkRunner runner, PlayerRef player) {

        if (!runner.IsServer) return;
        if (runner.SessionInfo.PlayerCount != LobbySettings.PlayerCount) return;
        StartGame();
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {

        Session.Close();
    }
    public void OnShutdown (NetworkRunner runner, ShutdownReason reason) {

        if (SceneManager.GetActiveScene().name.Equals(LobbySettings.GameSceneName))
            SceneManager.LoadScene("MainMenu");

        Session = null;
    }
    public void OnSceneLoaded (NetworkRunner runner) {

        if (!Session.Runner.IsServer) return;
        if (SceneManager.GetActiveScene().name != LobbySettings.GameSceneName) return;
        if (Session.Runner.ActivePlayers.Count() != LobbySettings.PlayerCount) return;

        var logic = Session.Runner.Spawn(GameLogicManagerPrefab);
        OnGameLogicManagerCreated?.Invoke(logic.GetComponent<GameLogicManager>());
    }

}
