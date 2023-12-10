
/*
    The purpose of this file to define enums
    name constants from Assets
*/ 

public class Const
{
    // Layer name & ID
    public enum Layers
    {
        Player = 6,
        Enemy = 7
    }
    
    // Tags name & ID
    public enum Tags
    {
        PlayerSlot,
        PlayerSlime,
        EnemySlot,
        EnemySlime,
        Arena
    }
    
    // Sprite name from Assets/UI/Slimes
    // Sprites are ordered as level increased
    public enum PlayerSpritesMelee
    {
        Melee1,
        Melee2,
        Melee3,
        Melee4,
        Melee5,
        Melee6,
        Melee7,
        Melee8,
        Melee9,
        Melee10
    }
    public enum PlayerSpritesRange
    {
        Archer1,
        Archer2,
        Archer3,
        Archer4,
        Archer5,
        Archer6,
        Archer7,
        Archer8,
        Archer9,
        Archer10
    }
    public enum SlimeSpritesMelee
    {
        SlimeMelee1,
        SlimeMelee2,
        SlimeMelee3,
        SlimeMelee4,
        SlimeMelee5,
        SlimeMelee6,
        SlimeMelee7,
        SlimeMelee8,
        SlimeMelee9,
        SlimeMelee10
    }
    public enum SlimeSpritesRange
    {
        SlimeArcher1,
        SlimeArcher2,
        SlimeArcher3,
        SlimeArcher4,
        SlimeArcher5,
        SlimeArcher6,
        SlimeArcher7,
        SlimeArcher8,
        SlimeArcher9,
        SlimeArcher10
    }
    public enum SlotSprites
    {
        SlotForest1,
        SlotForest2,
        SlotDesert1,
        SlotDesert2,
        SlotSnow1,
        SlotSnow2
    }

    public enum MeleeHp
    {
        Level1 = 45,
        Level2 = 115,
        Level3 = 270,
        Level4 = 625,
        Level5 = 1320,
        Level6 = 2675,
    }
    public enum MeleeDamage
    {
        Level1 = 5,
        Level2 = 11,
        Level3 = 24,
        Level4 = 55,
        Level5 = 115,
        Level6 = 150,
    }
    
    public enum RangeHp
    {
        Level1 = 18,
        Level2 = 43,
        Level3 = 105,
        Level4 = 255,
        Level5 = 605,
        Level6 = 1320,
        Level7 = 2765,
        Level8 = 5730
    }
    public enum RangeDamage
    {
        Level1 = 5,
        Level2 = 12,
        Level3 = 27,
        Level4 = 64,
        Level5 = 152,
        Level6 = 315,
        Level7 = 645,
        Level8 = 1305   
    }
}
