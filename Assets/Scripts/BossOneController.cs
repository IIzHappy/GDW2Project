using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossOneController : EnemyController
{
    public GameObject BossBeam;
    public GameObject enemyM;
   

   
    protected override void Attack()
    {
        Vector2 BeamPosition = transform.position;
        if( player.transform.position.x >  transform.position.x)
        {
            BeamPosition += new Vector2(10f, 0f);
        }
        if (player.transform.position.x < transform.position.x)
        {
            BeamPosition -= new Vector2(10f, 0f);
        }
        GameObject Beam = Instantiate(BossBeam);
        Beam.transform.position = BeamPosition;
        Beam.transform.parent = transform;
        moveSpeed = 0f;
        


    }

    protected override void FindPlayer()
    {
        toPlayer = player.transform.position - transform.position;
        if (player.transform.position.y >= transform.position.y - 5f && player.transform.position.y <= transform.position.y +5f )
        {
            inRange = true;

        }else
        {
            inRange = false;
        }
       // inRange = (range);

    }

    private void OnDestroy()
    {
        enemyM.GetComponent<enemyManager>().bossKilled = true;
        Debug.Log("boss is dead");
    }
}
