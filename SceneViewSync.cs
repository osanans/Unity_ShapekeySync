using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// 1. 오브젝트에 붙는 컴포넌트 부분
public class SceneViewSync : MonoBehaviour
{
    [Tooltip("체크하면 씬 뷰 시점과 실시간으로 동기화됩니다. (플레이 모드에서도 작동)")]
    public bool 실시간동기화 = false;
}

// 2. 에디터에서 실시간 동기화를 처리하는 관리자 부분
#if UNITY_EDITOR
[InitializeOnLoad]
public static class SceneViewSyncManager
{
    static SceneViewSyncManager()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        // [수정됨] 플레이 모드 방어 코드를 삭제하여 언제든 작동하게 만듭니다.
        // if (Application.isPlaying) return; 

        SceneViewSync[] syncTargets = Object.FindObjectsOfType<SceneViewSync>();

        foreach (SceneViewSync sync in syncTargets)
        {
            if (sync != null && sync.실시간동기화)
            {
                sync.transform.position = sceneView.camera.transform.position;
                sync.transform.rotation = sceneView.camera.transform.rotation;
            }
        }
    }
}
#endif