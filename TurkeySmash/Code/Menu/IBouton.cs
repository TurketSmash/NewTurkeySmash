using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    public interface IBouton
    {
        bool Etat { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
