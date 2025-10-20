using UnityEngine;

public class DeathRecenter : Death
{
    public override void Die()
    {
        //Moves Object Back to 0,0,0
        transform.position = Vector3.zero;

        base.Die();
    }
}
