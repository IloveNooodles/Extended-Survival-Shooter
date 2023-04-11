public class EnemyName
{
    public static string Zombunny = "zombunny";
    public static string Zombear = "zombear";
    public static string Hellepant = "hellephant";
    public static string Titan = "titan";
    public static string Wizard = "wizard";

    public static int GetEnemyId(string name)
    {
        if (name == Zombunny) return 0;
        if (name == Zombear) return 1;
        if (name == Hellepant) return 2;
        if (name == Titan) return 3;
        if (name == Wizard) return 4;

        return 0;
    }
}