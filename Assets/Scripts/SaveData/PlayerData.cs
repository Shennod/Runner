namespace SaveData
{
    [System.Serializable]
    public class PlayerData
    {
        public bool IsFirstRun = true;
        public int Coins;
        public bool[] IsBuyed = new bool[5];      
    }
}