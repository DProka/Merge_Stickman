
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float _speed;
    private Slime target;
    private int _damage;

    private void Start()
    {
        GameController.gameController.projectList.Add(this);
    }

    public void UpdateProj()
    {
        FindEnemyPosition();

        transform.position += (Time.fixedDeltaTime * transform.right).normalized * _speed;
        if (target != null)
        {
            float dir = Vector2.Distance(target.transform.position, transform.position);
            if (dir < 0.1f)
            {
                target.GetHit(_damage);
                DestroyGO();
            }
        }

        if (!GameController.gameController.arenaActive)
        {
            DestroyGO();
        }
    }

    public void SetStats(Slime enemy, int damage)
    {
        target = enemy;
        _damage = damage;
    }

    public void FindEnemyPosition()
    {
        if(target.isDead || target == null)
        {
            DestroyGO();
        }

        if (!target.isDead && target != null)
        {
            Vector2 direction = (target.transform.position - transform.position);
            float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }    
    }

    private void DestroyGO()
    {
        GameController.gameController.projectList.Remove(this);
        Destroy(gameObject);
    }
}
