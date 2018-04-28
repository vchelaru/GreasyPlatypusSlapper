
namespace GreasyPlatypusSlapper.DataTypes
{
    public partial class FeatureFlags
    {
        public string Name;
        public bool IsEnabled;
        public const string EnableTankTreads = "EnableTankTreads";
        public const string EnableRocketTrails = "EnableRocketTrails";
        public const string EnableTurnBasedMovement = "EnableTurnBasedMovement";
        public const string EnableBoost = "EnableBoost";
        public const string EnableSmokeOnLowHealth = "EnableSmokeOnLowHealth";
        public static System.Collections.Generic.List<System.String> OrderedList = new System.Collections.Generic.List<System.String>
        {
        EnableTankTreads
        ,EnableRocketTrails
        ,EnableTurnBasedMovement
        ,EnableBoost
        ,EnableSmokeOnLowHealth
        };
        
        
    }
}
