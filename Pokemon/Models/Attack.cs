using Pokemon.AdditionalEffects;
using Pokemon.Calculators;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon.Models
{
    public class Attack : IAttack
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public int? Power { get; set; }

        public int? Accuracy { get; set; }

        public string BoostStats { get; set; }

        public ElementalType TypeID { get; set; }

        public bool IsSpecial { get; set; }

        public ICollection<IAdditionalEffect> AdditionalEffects { get; set; }

        public int CalculateDamage(IPokemon pokemon, IPokemon target)
        {
            int damage = 0;

            if (AdditionalEffects.ContainsEffectType(typeof(AlwaysSameDamage)))
            {
                AlwaysSameDamage alwaysSameDamage = AdditionalEffects.First(e => e is AlwaysSameDamage) as AlwaysSameDamage;
                damage = alwaysSameDamage.IsBasedOnLevel() ? pokemon.Level : (int)alwaysSameDamage.PrimaryParameter;
            }

            if (damage == 0 && Power.HasValue)
            {
                damage = DamageCalculator.CalculateAttackDamage(this, pokemon, target);
                if (damage < 1)
                {
                    damage = 1;

                }
                if (BattleHelper.IsCritical(AdditionalEffects, pokemon.IsEnergyFocused))
                {
                    damage *= 2;
                    //_battleLogController.SetText("Critical hit!");
                }
            }
            return damage;
        }

        public bool Missed()
        {
            if (AdditionalEffects.ContainsEffectType(typeof(AlwaysHits))) return false;

            return !ChanceCalculator.CalculateChance(Accuracy.Value);

        }
    }
}
