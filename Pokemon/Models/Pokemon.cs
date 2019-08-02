using Pokemon.Calculators;
using Pokemon.Factory;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Pokemon.Models
{
    [DataContract]
    public class Pokemon : IPokemon
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int HPCurrent { get; set; }

        [DataMember]
        public int HPMax { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public IPokemonStats Stats { get; set; }

        [DataMember]
        public IList<IAttack> Attacks { get; set; }

        [DataMember]
        public Condition Condition { get; set; }

        public bool IsFlinched { get; set; }

        public bool IsEnergyFocused { get; set; }

        public bool IsConfused { get; set; }

        [DataMember]
        public int PrimaryTypeID { get; set; }

        [DataMember]
        public int? SecondaryTypeID { get; set; }

        public int[] StatModifierStages { get; set; }

        public int MinimalLevel { get; set; }

        public Pokemon()
        {
            StatModifierStages = new int[5] { 0,0,0,0,0};
        }

        public void Hurt(int value)
        {
            HPCurrent -= value;
        }

        public void Heal(int value)
        {
            if (HPCurrent + value > HPMax)
            {
                HPCurrent = HPMax;
            }
            else
            {
                HPCurrent += value;
            }
        }

        public bool IsPokemonAlive() => HPCurrent > 0;

        public void ResetStats()
        {
            for (int i = 0; i < StatModifierStages.Length; i++)
            {
                StatModifierStages[i] = 0;
            }

            IsConfused = false;
            IsFlinched = false;
            IsEnergyFocused = false;
        }

        public object Clone()
        {
            IPokemon pokemon = PokemonFactory.CreatePokemon();

            foreach (PropertyInfo property in pokemon.GetType().GetProperties())
            {
                property.SetValue(pokemon, property.GetValue(this, null), null);
            }
            pokemon.ResetStats();
            return pokemon;
        }

        public bool IsAbleToAttackAfterConditionEffect()
        {
            int damage;
            switch (Condition)
            {
                case 0:
                    return true;

                case Condition.BRN:
                    damage = HPMax / 16;
                    Hurt(damage);
                    //_battleLogController.SetText($"{pokemon.Name} is burning (Damage: {damage})");
                    return IsPokemonAlive();

                case Condition.FRZ:
                    //_battleLogController.SetText($"{pokemon.Name} is frozen");
                    return false;

                case Condition.PAR:
                    if (ChanceCalculator.CalculateChance(50)) return true;
                    //_battleLogController.SetText($"{pokemon.Name} is unable to move");
                    return false;

                case Condition.PSN:
                    damage = HPMax / 16;
                    Hurt(damage);
                    //_battleLogController.SetText($"{pokemon.Name} is hurt by poison (Damage: {damage})");
                    return IsPokemonAlive();

                case Condition.SLP:
                    if (ChanceCalculator.CalculateChance(50))
                    {
                        //_battleLogController.SetText($"{pokemon.Name} woke up");
                        Condition = 0;
                        return true;
                    }
                    //_battleLogController.SetText($"{pokemon.Name} is still sleeping");
                    return false;

                default:
                    return true;
            }
        }

        public bool IsAbleToAttack()
        {
            if (IsFlinched)
            {
                IsFlinched = false;
                //_battleLogController.SetText($"{attackingPokemon.Name} is flinched");
                return false;
            }

            if (IsConfused && BattleHelper.HasFailedConfusion(this))
            {
                var confusionHit = PokemonAttacksFactory.CreateAttack("ConfusionHit");
                int damage = DamageCalculator.CalculateAttackDamage(confusionHit, this, this);
                Hurt(damage);

                //_battleLogController.SetText($"{attackingPokemon.Name} hurts itself in its confusion");
                return IsPokemonAlive();
            }

            return true;
        }
    }
}
