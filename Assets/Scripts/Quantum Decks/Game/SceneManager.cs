using UnityEngine;

namespace Quantum_Decks.Game
{
   public class SceneManager : MonoBehaviour
   {
      public void LoadScene(string sceneId)
      {
         UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
      }

      public void QuitGame()
      {
         Application.Quit();
      }
   }
}
