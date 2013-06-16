using Microsoft.Xna.Framework;

namespace TurkeySmash
{
    class FarseerBodyUserData
    {
        public int Pourcent { get; set; }
        public int LastHit { get; set; }
        public string AssociatedName { get; set; }
        public bool Protecting { get; set; }
        public bool IsCharacter { get; set; }
        public bool IsBonus { get; set; }
        public bool IsUsed { get; set; }
        public bool Invicible { get; set; }

        public string BonusType { get; set; }
            
    }
}
