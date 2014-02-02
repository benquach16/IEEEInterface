using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    //setting up more hierarchy
    class UIButton : WinFormsGraphicsDevice.UIElement
    {
        public UIButton(Vector2 position, Vector2 size, UIElement parent) :
            base(position, size, parent, E_UI_TYPES.UI_BUTTON)
        {
        }
    }
}
