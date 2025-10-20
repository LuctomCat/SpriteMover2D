using UnityEngine;

public class DeathMechanics : Death
{
    public override void Die()
    {
        //Destroy Player Object
        Destroy(gameObject);
    }
}
