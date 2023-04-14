using System;
using System.Collections;
using UnityEngine;


public class Arrow : Weapon
{
    private Vector3 _startPoint;
   
    public override void Shoot(Vector3 direction)
    {
        _startPoint = transform.position;
        
        Activate();

        StartCoroutine(MoveRoutine(direction));
       
    }
    
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log("Arrow collision: damage -" + card.damage);
    //
    //     PlayerManager.Instance.ReleaseWeapon(card);
    //     
    //     //Destroy(gameObject);
    //
    //     // if (collision.gameObject.CompareTag("Field"))
    //     // {
    //     //     Destroy(this.gameObject);
    //     //
    //     //     //PoolManager.Instance.Push(this.gameObject.transform);
    //     // }
    // }

    private IEnumerator MoveRoutine(Vector3 direction)
    {
        while (Vector3.Distance(transform.position, _startPoint) < card.distance)
        {
            Rigidbody.MovePosition(transform.position + direction * (card.speed * Time.deltaTime));
            
            yield return null;
        }
        
        PlayerManager.Instance.ReleaseWeapon(this);
    }
}