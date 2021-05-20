using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfHeroes = int.Parse(Console.ReadLine());
            Dictionary<string, int> heroHit = new Dictionary<string, int>();
            Dictionary<string, int> heroMana = new Dictionary<string, int>();

            for (int i = 1; i <= numberOfHeroes; i++)
            {
                string[] hero = Console.ReadLine().Split(" ");
                string heroName = hero[0];
                int hitPoints = int.Parse(hero[1]); 
                int manaPoints = int.Parse(hero[2]); 
                heroHit[heroName] = hitPoints;
                heroMana[heroName] = manaPoints;
            }

            string command = "";
            while ((command = Console.ReadLine()) != "End")
            {
                string[] action = command.Split(" - ");
                string toDo = action[0];
                string heroName = action[1];
                if (toDo == "CastSpell")
                {
                    int manaNeeded = int.Parse(action[2]);
                    string spellName = action[3];
                    if (heroMana[heroName]>=manaNeeded)
                    {
                        heroMana[heroName] -= manaNeeded;
                        Console.WriteLine($"{heroName} has successfully cast {spellName} and now has {heroMana[heroName]} MP!");
                    }
                    else
                    {
                        Console.WriteLine($"{heroName} does not have enough MP to cast {spellName}!");
                    }
                }
                else if (toDo == "TakeDamage")
                {
                    int damage = int.Parse(action[2]);
                    string attacker = action[3];
                    heroHit[heroName] -= damage;
                    if (heroHit[heroName]>0)
                    {
                        Console.WriteLine($"{heroName} was hit for {damage} HP by {attacker} and now has {heroHit[heroName]} HP left!");
                    }
                    else
                    {
                        heroHit.Remove(heroName);
                        heroMana.Remove(heroName);
                        Console.WriteLine($"{heroName} has been killed by {attacker}!");
                    }

                }
                else if (toDo == "Recharge")
                {
                    int amount = int.Parse(action[2]);
                    heroMana[heroName] += amount;
                    if (heroMana[heroName]>200)
                    {
                        amount = amount - (heroMana[heroName] - 200);
                        heroMana[heroName] = 200;
                    }
                    Console.WriteLine($"{heroName} recharged for {amount} MP!");
                }
                else if (toDo == "Heal")
                {
                    int amount = int.Parse(action[2]);
                    heroHit[heroName] += amount;
                    if (heroHit[heroName] > 100)
                    {
                        amount = amount - (heroHit[heroName] - 100);
                        heroHit[heroName] = 100;
                    }
                    Console.WriteLine($"{heroName} healed for {amount} HP!");
                }
            }

            foreach (var hero in heroHit.OrderByDescending(x=>x.Value).ThenBy(x=>x.Key))
            {
                string name = hero.Key;
                Console.WriteLine(name);
                Console.WriteLine($"  HP: {heroHit[name]}");
                Console.WriteLine($"  MP: {heroMana[name]}");
            }
        } 
    }
}
