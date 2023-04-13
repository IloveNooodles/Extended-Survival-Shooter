public class ItemName
{
    public static string Gold = "Gold";
    public static string Bullet = "Bullet";

    public static int ItemId(string name)
    {
        if (name == Gold) return 1;
        if (name == Bullet) return 2;
        
        return 0;
    }
}
