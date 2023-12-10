
using UnityEngine;

public class UnitRange : Slime
{
    public Projectile projectilePrefab;

    private void Start()
    {
        attackTimer = _attackSpeed;
    }

    public override void Action()
    {
        if (_enemy is null)
        {
            FindEnemy();
        }

        if (_enemy != null && !isDead)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.fixedDeltaTime;
                healthBar.UpdateAttackBar(attackTimer, _attackSpeed);
            }
            else
            {
                ThrowProjectile();
            }
            _enemy = _enemy.isDead ? null : _enemy;
        }
    }

    void ThrowProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform);
        projectile.SetStats(_enemy, _damage);
        attackTimer = _attackSpeed;
        SetCharacterState("Attacks");
        if (_isPlayerSlime)
            GameController.gameController.soundController.PlayUnitAttack(false);
    }
}
