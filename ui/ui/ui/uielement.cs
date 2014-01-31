using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace ui
{
    //abstract base class
    class UIElement
    {
        enum E_UI_TYPES
        {
            //for oop

        };
        //setup class hierarchy
        protected Vector2 position;
        protected Vector2 size;

        UIElement()
        {
            
        }

        Vector2 getSize()
        {
            return size;
        }
    }
}
