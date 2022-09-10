using Fusion;

[System.Serializable]
public class UserInfo
{
    public Data data;

    [System.Serializable]
    public class Data
    {
        public int b;
        [Networked] public string first_name { get; set; }
        [Networked] public string last_name { get; set; }
        [Networked] public string photo_100 { get; set; }
    }
}