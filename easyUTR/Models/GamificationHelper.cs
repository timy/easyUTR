namespace easyUTR.Helpers
{
    public static class GamificationHelper
    {
        public static string GetTierFromPoints(int points)
        {
            if (points >= 1000) return "Gold";
            if (points >= 500) return "Silver";
            if (points >= 100) return "Bronze";
            return "Regular";
        }

        public static string GetTierIcon(int points)
        {
            string tier = GetTierFromPoints(points);
            return tier switch
            {
                "Gold" => "👑",
                "Silver" => "🥈",
                "Bronze" => "🥉",
                _ => ""
            };
        }
    }
}