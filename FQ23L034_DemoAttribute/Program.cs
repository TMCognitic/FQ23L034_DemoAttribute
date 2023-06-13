// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Reflection;

Hero hero = new Hero();

hero.Loot(new Dragon());

class Hero
{
    public void Loot(Monstre monstre)
    {
        Type type = monstre.GetType();

        IEnumerable<LootAttribute> lootAttributes = type.GetCustomAttributes<LootAttribute>();

        foreach (LootAttribute item in lootAttributes)
        {
            Console.WriteLine($"{item.Quantite} {item.Nom}");
        }
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited=true)]
abstract class LootAttribute : Attribute
{
    private int _maximum;

    public int Quantite
    {
        get { return Random.Shared.Next(_maximum); }
    }

    public string Nom { get; init; }

    public LootAttribute(int maximum, string nom)
    {
        Nom = nom;
        _maximum = maximum;
    }
}

class CuirAttribute : LootAttribute
{
    public CuirAttribute() : this(5, "morceau(x) de cuir")
    {
        
    }

    public CuirAttribute(int maximum) : this(maximum, "morceau(x) de cuir")
    {

    }

    public CuirAttribute(int maximum, string nom) : base(maximum, nom)
    {

    }
}

class OrAttribute : LootAttribute
{
    public OrAttribute() : base(7, "pièce(s) d'or")
    {

    }

    public OrAttribute(int maximum) : base(maximum, "pièce(s) d'or")
    {

    }
}

class GemmeAttribute : LootAttribute
{
    public GemmeAttribute() : base(2, "gemme(s)")
    {

    }

    public GemmeAttribute(int maximum) : base(maximum, "gemme(s)")
    {

    }
}

[Gemme]
abstract class Monstre
{

}

[Cuir]
class Loup : Monstre
{
   
}

[Or]
class Orc : Monstre
{
    
}

[Cuir(40, "écaille(s) de dragon")]
[Or(600)]
[Gemme(100)]
class Dragon : Monstre
{
    
}
