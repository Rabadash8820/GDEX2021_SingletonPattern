using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyClone.Shared
{
    public class GameLifecycleManager : MonoBehaviour
    {
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "UnityEvents can't call static methods")]
        public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "UnityEvents can't call static methods")]
        public void Quit() => Application.Quit();
    }
}
