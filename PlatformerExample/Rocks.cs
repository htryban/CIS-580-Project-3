using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerExample
{
    public class Rocks : IBoundable
    {
        /// <summary>
        /// The platform's bounds
        /// </summary>
        BoundingRectangle bounds;

        /// <summary>
        /// The platform's sprite
        /// </summary>
        Sprite sprite;


        // <summary>
        /// The bounding rectangle of the 
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Constructs a new rock
        /// </summary>
        /// <param name="bounds">The platform's bounds</param>
        /// <param name="sprite">The platform's sprite</param>
        public Rocks(BoundingRectangle bounds, Sprite sprite)
        {
            this.bounds = bounds;
            this.sprite = sprite;
        }

        public void fall(int x, int y, float speed)
        {
            if (bounds.Y < 1100) bounds.Y += 10 * speed;
            else
            {
                bounds.Y = y;
                bounds.X = x;
            }
        }

        /// <summary>
        /// Draws the rock
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to render to</param>
        public void Draw(SpriteBatch spriteBatch)
        { 
            sprite.Draw(spriteBatch, new Vector2(bounds.X, bounds.Y), Color.White);
        }

    }
}
