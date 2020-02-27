using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerExample
{
    class PlayerState
    {
        ~PlayerState() { }
        public static JumpState jump;
        public virtual void takeInput(Player player, string input) 
        {
            if(input == "jumping left")
            {
                jump.takeInput(player, input);
                //player.state_ = PlayerState::jumping;
            }
        }
        public virtual void update(Player player) { }
    }
}
