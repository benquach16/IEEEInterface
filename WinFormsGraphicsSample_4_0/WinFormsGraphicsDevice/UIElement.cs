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
        //static list for all the ui elements
        public enum E_UI_TYPES
        {
            //for oop
            UI_BUTTON,
            UI_WINDOW,
            UI_STATIC_TEXT,
            UI_EMPTY
        };
        //setup class hierarchy
        protected Vector2 position;
        protected Vector2 size;
        protected E_UI_TYPES type;
        protected bool visible;

        protected UIElement parent;
        protected List<UIElement> children;


        public UIElement(Vector2 position, Vector2 size, UIElement parent, E_UI_TYPES type)
        {
            this.position = position;
            this.size = size;
            this.type = type;
            this.visible = true;
        }
        public virtual void draw(SpriteBatch batch)
        {
            //draw this and all children
            for (int i = 0; i < children.Count; i++)
            {
                children[i].draw(batch);
            }
        }

        public E_UI_TYPES getUIType()
        {
            return type;
        }

        //mutators can go ehre
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void setSize(Vector2 newSize)
        {
            size = newSize;
        }

        //some accessors go here
        public Vector2 getPosition()
        {
            return position;
        }
        public Vector2 getSize()
        {
            return size;
        }
        public UIElement getParent()
        {
            return parent;
        }
        public List<UIElement> getChildren()
        {
            return children;
        }
    }
}
