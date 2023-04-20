using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace SnakeAndLadder
{
    public class SceneLoader : MonoBehaviour
    {

        [MenuItem("Scene/Menu Scene")]
        static void OpenMenuScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/MenuScene.unity");
        }

        [MenuItem("Scene/Game Scene")]
        static void OpenGameScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/GameScene.unity");
        }
    }
}