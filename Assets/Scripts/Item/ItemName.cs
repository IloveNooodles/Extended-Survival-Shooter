public class ItemName
{
    public static string Gold = "Gold";
    public static string Bullet = "Bullet";

    public static int ItemId(string name)
    {
        if (name == Gold) return 0;
        if (name == Bullet) return 1;
        
        return 0;
    }
}
