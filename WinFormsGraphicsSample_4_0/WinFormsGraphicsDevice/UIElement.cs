using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    class UIElement
    {
        enum E_UI_TYPES
        {
            //for oop
            UI_BUTTON,
            UI_WINDOW,
            UI_STATIC_TEXT
        };
        //setup class hierarchy
        protected Vector2 position;
        protected Vector2 size;
        protected E_UI_TYPES type;
        UIElement(Vector2 position, Vector2 size, E_UI_TYPES type)
        {
            this.position = position;
            this.size = size;
            this.type = type;
        }
        //PURE VIRTUAL FUNCTION
        public abstract void draw();
        //PURE VIRTUAL FUNCTION
        public abstract E_UI_TYPES getUIType();

        Vector2 getPosition()
        {
            return position;
        }
        Vector2 getSize()
        {
            return size;
        }
    }
}
