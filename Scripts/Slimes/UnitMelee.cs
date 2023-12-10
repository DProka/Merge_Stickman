
using UnityEngine;

public class UnitMelee : Slime
{
    public float speed = 1.5f;

    private void Start()
    {
        attackTimer = 0.1f;
    }

    public override void Action()
    {
        if (_enemy != null && !_enemy.isDead)
        {
            _enemy = _enemy.isDead ? null : _enemy;

            Vector3 enemypos = new Vector3();

            if (_enemy.transform.position.x > transform.position.x)
            {
                enemypos = CheckEnemyPos(true);
            }
            else if (_enemy.transform.position.x < transform.position.x)
            {
                enemypos = CheckEnemyPos(false);
            }
            else
            {
                if(transform.position.x < 2.5f)
                {
                    enemypos = CheckEnemyPos(false);
                }
                else
                {
                    enemypos = CheckEnemyPos(true);
                }
            }
            
            Vector2 dir = (enemypos - transform.position).normalized;
            float dist = Vector2.Distance(enemypos, transform.position);

            if (dist > 0.1f)
            {
                transform.Translate(dir * speed * Time.deltaTime);
                SetCharacterState("Walking");
            }
            else
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.fixedDeltaTime;
                    healthBar.UpdateAttackBar(attackTimer, _attackSpeed);
                }
                else
                    DealDamage();
            }
        }
        else
        {
            FindEnemy();
            attackTimer = 0;
        }
    }

    public Vector3 CheckEnemyPos(bool left)
    {
        Vector3 enemypos = new Vector3();

        if (left)
        {
            enemypos = new Vector3(_enemy.transform.position.x - 0.6f, _enemy.transform.position.y, 0);
        }
        else
        {
            enemypos = new Vector3(_enemy.transform.position.x + 0.6f, _enemy.transform.position.y, 0);
        }

        return enemypos;
    }

    public void DealDamage()
    {
        _enemy.GetHit(_damage);
        attackTimer = _attackSpeed;
        if (_isPlayerSlime)
            GameController.gameController.soundController.PlayUnitAttack(true);
        SetCharacterState("Attacks");
    }
}
