using System.Collections.Generic;

namespace Pokemon.Models
{
    public interface IBattle
    {
        IPokemon PlayerPokemon{ get; set; }

        IPokemon EnemyPokemon{ get; set; }
        
        void PerformAttack(IAttack attack);

        IAttack GetEnemyAttack();

        IPokemon GetFasterPokemon(
            ICollection<IAdditionalEffect> playerPokemonAdditionalEffects,
            ICollection<IAdditionalEffect> enemyPokemonAdditionalEffects);


    }
}