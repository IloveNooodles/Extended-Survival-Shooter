﻿public class EnemyName
{
    public static string Zombunny = "Zombunny";
    public static string Zombear = "Zombear";
    public static string Hellepant = "Hellepant";
    public static string Titan = "Titan";
    public static string Wizard = "Wizard";

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