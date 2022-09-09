using Fusion;

[System.Serializable]
public class UserInfo
{
    public class City
    {
        [Networked] public string title { get; set; }
    }

    public class Data
    {
        [Networked] public string first_name { get; set; }
        [Networked] public string last_name { get; set; }
        [Networked] public string photo_100 { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }
}