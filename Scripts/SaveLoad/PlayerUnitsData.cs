using UnityEngine;

namespace SaveData
{
    [System.Serializable]
    public class PlayerUnits
    {
        public GameObject[] _playerUnits;
        public int[] _isMeleeArray;
        public int[] _unitLvlArray;

        public PlayerUnits()
        {
            _playerUnits = new GameObject[15];
            _isMeleeArray = new int[15];
            _unitLvlArray = new int[15];
        }
    }
}


