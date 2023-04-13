using UnityEngine;

public class Aiming : MonoBehaviour
{
    public static Transform Aim(Rigidbody2D rigidbody2d, WeaponCard weaponCard, int layerMask)
    {
        Transform result = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(rigidbody2d.transform.position, weaponCard.distance, 1 << LayerMask.NameToLayer("Enemy"));

        foreach (var collider in colliders)
        {
            Debug.Log($"{collider.gameObject.name} is nearby");

            result = collider.gameObject.transform;

            //Vector2 enemyPosition = collider.transform.position;
            //Vector2 enemyDirection = enemyPosition - rigidbody2d.position;

            ////raycasr
            //RaycastHit2D hit;
            //Physics2D.Raycast(rigidbody2d.position, enemyDirection, LayerMask.NameToLayer("Wall"),);
            //if (!target)
            //{
            //    result = target.collider.gameObject.transform;
            //    return result;
            //}
            //else
            //{
            //    Debug.Log($"found wall");
            //}


            //if no wall contacts in raycast => return set target
        }

        return result;
    }
}
