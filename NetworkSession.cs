
using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkSession : MonoBehaviour {

    public string sessionName;
    private NetworkRunner runner;
    private NetworkSceneManagerDefault sceneManager;
    public NetworkManager manager;
    public LobbySettings lobbySettings;
    public bool IsServer;

    private void Awake() {
        
        runner = GetComponent<NetworkRunner>();
        sceneManager = GetComponent<NetworkSceneManagerDefault>();
    }
    public async Task Open (GameMode mode) {

        if (runner == null) return;
        runner.AddCallbacks(NetworkManager.Instance.Callbacks);
        await runner.StartGame(GetArgs(mode));
        IsServer = runner.IsServer;
        sessionName = runner.SessionInfo.Name;
        gameObject.name = $"Session: {runner.SessionInfo.Name.Substring(0, 5)}";
    }
    public void Close () {

        if (runner == null) return;
        if (!runner.IsRunning) return;

        runner.Shutdown();
    }
    private StartGameArgs GetArgs (GameMode mode) {

        return new StartGameArgs {
            GameMode = mode,
            PlayerCount = lobbySettings.PlayerCount,
            SceneManager = sceneManager,
        };
    }

    public NetworkRunner Runner => runner;
}
