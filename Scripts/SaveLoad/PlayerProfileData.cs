namespace SaveData
{
    [System.Serializable]
    public class PlayerProfile
    {
        public int _level;
        public ulong _money;
        public uint _meleeCounter;
        public uint _rangeCounter;
        public int _mapSpriteNumber;

        public int _meleeOpened;
        public int _rangeOpened;

        public PlayerProfile()
        {
            _level = 0;
            _money = 500;
            _meleeCounter = 0;
            _rangeCounter = 0;
            _meleeOpened = 0;
            _rangeOpened = 0;
            _mapSpriteNumber = 0;
        }
    }
}