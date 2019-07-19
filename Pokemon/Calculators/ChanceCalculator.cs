namespace Pokemon.Calculators
{
    public static class ChanceCalculator
    {
        public static bool CalculateChance(int chance, int maxRange = 100)
        {
            return GenerateRandomNumber.GetRandomNumber(0, maxRange) <= chance;
        }
    }
}
