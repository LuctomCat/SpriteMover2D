using UnityEngine;

// Handles sound effects when asteroids are destroyed
public class AsteroidSoundEffects : MonoBehaviour
{
    private bool hasPlayedSound = false;

    void OnDestroy()
    {
        if (Application.isPlaying && !hasPlayedSound)
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayExplosion();
            }
            hasPlayedSound = true;
        }
    }
}
