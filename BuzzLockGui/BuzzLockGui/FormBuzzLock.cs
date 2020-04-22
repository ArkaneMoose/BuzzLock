using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuzzLockGui
{
    public class FormBuzzLock : Form
    {
        protected enum State
        {
            Uninitialized,
            Initializing,
            Idle,
            GenericOptions,
            Authenticated, //TODO: spin servo to unlock door in Authenticated, and keep it unlocked during UserOptions
            UserOptions,    //      and in Authenticated until timeout happens.
            UserOptions_EditProfile,
            UserOptions_EditAuth
        }

        protected static State _globalState;
    }
}
