using UnityEngine;
using System.Linq;

namespace GameSystems.Audio {
   public class AudioManager : MonoBehaviour {
      private AudioManager() { }

      public static AudioManager Instance { get; private set; }

      [SerializeField] private Sound[] sounds;

      private void Awake() {
         if (Instance is null) {
            Instance = this;
            DontDestroyOnLoad(this);

            foreach (var sound in sounds) {
               sound.AudioSource = gameObject.AddComponent<AudioSource>();
               sound.AudioSource.clip = sound.AudioClip;
               sound.AudioSource.volume = sound.Volume;
            }
         }
         else {
            Destroy(gameObject);
         }
      }

      public void PlayOneShot(string name) {
         var sound = sounds.FirstOrDefault(sound => sound.Name == name);
         if (sound is null) {
            Debug.LogError($"Sound named {name} not found.");
            return;
         }

         sound.AudioSource.PlayOneShot(sound.AudioClip);
      }
   }
}