using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public PlayerControl pawnToTest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pawnToTest.death.Die();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            pawnToTest.health.TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            pawnToTest.health.Heal(1);
        }
    }
}
