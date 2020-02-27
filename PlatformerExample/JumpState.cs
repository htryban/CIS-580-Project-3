using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerExample
{
    class JumpState : PlayerState
    {
        public virtual void takeInput(Player player, string input)
        {
            if(input == "jumping left")
            {
                update(player);
                //change some shit
            }
        }
        public virtual void update(Player player)
        {
            player.Anim(PlayerAnimState.JumpingLeft, 7);
        }
    }
}
