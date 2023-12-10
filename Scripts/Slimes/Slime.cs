using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Spine.Unity;

public class Slime : MonoBehaviour
{
    private SlimeBase slimeBase;

    [Header("Stats")]
    public string unitName;
    public int _level;

    public int _maxHealth;
    private int _health;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool _isPlayerSlime;
    [HideInInspector] public bool _isMelee;

    public int _damage;
    public float _attackSpeed;
    [HideInInspector] public float attackTimer;
    private float timeToFind = 0.3f;
    private float _timerToFind;

    [Header("Components")]
    public SpriteRenderer body;
    public HealthBar healthBar;
    public TextMeshProUGUI _slimeLevelText;
    public Vector3 _originPos;

    public SkeletonAnimation skeleton;
    public AnimationReferenceAsset Idle, Walking, Attacks;
    public string currentState;
    public string currentAnimation;

    public Slime _enemy;
    public Slot _slotParent;
    public Slot _slotToChange;

    private void Start()
    {
        _health = _maxHealth;
        slimeBase = SlimeBase.slimeBase;
        Vector3 originPos = gameObject.transform.position;
        originPos.z -= 0.1f;
        _originPos = originPos;
        _timerToFind = timeToFind;
        SetCharacterState("Idle");
    }

    public void UpdateUnit()
    {
        if (GameController.gameController.arenaActive && !isDead)
        {
            _timerToFind -= Time.fixedDeltaTime;
            if (_timerToFind <= 0)
            {
                FindEnemy();
                _timerToFind = timeToFind;
            }

            Action();
        }

        if (!GameController.gameController.arenaActive)
        {
            SetCharacterState("Idle");
        }
    }

    public virtual void Action() { }

    public void FindEnemy()
    {
        float currentDistance = float.MaxValue;
        List<Slime> slimeList = _isPlayerSlime ? GameController.gameController.enemyList : GameController.gameController.playerList;

        foreach (Slime enemy in slimeList)
        {
            if (!enemy.isDead)
            {
                if (_enemy is null)
                {
                    _enemy = enemy;
                    currentDistance = Vector2.Distance(gameObject.transform.position, _enemy.transform.position);
                }

                float nextEnemyDistance = Vector2.Distance(gameObject.transform.position, enemy.transform.position);

                if (currentDistance > nextEnemyDistance)
                {
                    _enemy = enemy;
                    currentDistance = nextEnemyDistance;
                }
            }
        }

        if(_enemy != null && !_enemy.isDead)
        {
            if (_enemy.transform.position.x > transform.position.x)
            {
                body.transform.localScale = new Vector2(-0.9f, 0.9f);
            }
            else
            {
                body.transform.localScale = new Vector2(0.9f, 0.9f);
            }
        }
    }

    public void GetHit(int damage)
    {
        if (_health > damage)
            _health -= damage;

        else
        {
            isDead = true;
            IsDead();
        }

        healthBar.UpdateHealthBar(_health, _maxHealth);
    }

    public void ResetSlimePos()
    {
        isDead = false;
        gameObject.transform.position = _originPos;
        _enemy = null;
        _health = _maxHealth;
        healthBar.UpdateHealthBar(_health, _maxHealth);
        attackTimer = _isMelee ? 0 : _attackSpeed;
        healthBar.UpdateAttackBar(attackTimer, _attackSpeed);
        gameObject.SetActive(true);
        body.transform.localScale = new Vector2(0.9f, 0.9f);
        SetCharacterState("Idle");
    }

    public void IsDead()
    {
        if (_health <= 0)
        {
            isDead = true;
        }
        gameObject.SetActive(false);

        if (_isPlayerSlime)
            GameController.gameController.soundController.PlayUnitDeath();
    }

    public void CheckUnitPos()
    {
        if (GameController.gameController.arenaActive)
        { return; }

        Vector3 unitpos = ConvertUnitpositionToInt(transform.position.x, transform.position.y);

        Slot[] playerSlots = GameController.gameController.playerArena._slots;

        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i].transform.position == unitpos)
            {
                _slotToChange = playerSlots[i];
            }
        }

        if (_slotToChange != null)
        {
            Slot parentSlot = _slotParent;
            Slot nextSlot = _slotToChange;

            if (nextSlot != parentSlot)
            {
                if (nextSlot.isEmpty)
                {
                    parentSlot.SetSlime(null);
                    nextSlot.SetSlime(this);
                    _slotParent = nextSlot;
                    transform.position = nextSlot.position.position;
                    _originPos = transform.position;
                }
                else
                {
                    Slime slime = nextSlot.slimeScr;
                    if (slime._level == _level && slime._isMelee == _isMelee)
                    {
                        parentSlot.SetSlime(null);
                        nextSlot.SpawnSlime(_isPlayerSlime, _isMelee, _level + 1);
                        GameController.gameController.playerData.UnitOpened(_isMelee, _level);
                        Instantiate(SlimeBase.slimeBase.mergeAnimation, nextSlot.position);
                        GameController.gameController.RemoveFromList(slime);
                        Destroy(slime.gameObject);
                        GameController.gameController.RemoveFromList(this);
                        Destroy(gameObject);

                        GameController.gameController.soundController.PlayUnitUp();

                    }
                }
            }

            _slotToChange = null;
        }

        _originPos = new Vector3(_originPos.x, _originPos.y, 0);
        transform.position = _originPos;
        attackTimer = 0;
        GameController.gameController.ResetPlayerUnits();
    }

    public Vector3Int ConvertUnitpositionToInt(float x, float y)
    {
        int x1 = (int)x;
        int y1 = (int)y;
        Vector3Int vector3Int = new Vector3Int(x1, y1);

        return vector3Int;
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        if (animation.name.Equals(currentAnimation) && animation.name != "Attacks")
        {
            return;
        }
        skeleton.state.SetAnimation(0, animation, loop).TimeScale = timescale;
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        if (skeleton != null)
        {
            if (state.Equals("Idle"))
            {
                SetAnimation(Idle, true, 1f);
            }
            else if (state.Equals("Walking"))
            {
                SetAnimation(Walking, true, 1f);
            }
            else if (state.Equals("Attacks"))
            {
                SetAnimation(Attacks, false, 1f);
            }
        }
    }
}