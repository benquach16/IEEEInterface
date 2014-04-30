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
            UI_IMAGE,
            UI_ANIMATION,
            UI_GRAPH,
            UI_EMPTY
        };
        //setup class hierarchy
        protected Vector2 position;
        protected Vector2 absolutePosition;
        protected Vector2 size;
        protected E_UI_TYPES type;
        protected bool visible;
        protected float alpha;
        protected int transitionTimer;

        protected UIElement parent;
        protected List<UIElement> children;

        //for transitions
        protected bool moved;
        protected bool stop;
        protected float oldX;

        //base function
        public UIElement(Vector2 position, Vector2 size, UIElement parent, E_UI_TYPES type)
        {
            this.transitionTimer = 0;
            this.position = position;
            this.size = size;
            this.type = type;
            this.visible = true;
            this.parent = parent;
            this.moved = false;
            this.stop = false;
            this.children = new List<UIElement>();
            //yay for c++ design pattersn
            if (parent != null)
            {
                parent.addChild(this);
                absolutePosition = parent.getPosition() + position;
            }
            else
            {
                absolutePosition = position;
            }
        }
        //overload the constructor
        public UIElement(Vector2 position, UIElement parent, E_UI_TYPES type)
        {
            this.position = position;
            this.type = type;
            this.visible = true;
            this.parent = parent;
            this.children = new List<UIElement>();
            //yay for c++ design pattersn
            if (parent != null)
            {
                parent.addChild(this);
                absolutePosition = parent.getPosition() + position;
            }
            else
            {
                absolutePosition = position;
            }
        }
        //destructor
        //very important fucntion
        ~UIElement()
        {
            if (this.parent != null)
            {
                //if we have a parent remove this from its children
                this.parent.removeChild(this);
            }
            //delete all chidlren
            for (int i = 0; i < children.Count; i++)
            {
                children[i] = null;
            }
            //do garbage collection
            GC.Collect();
            children.TrimExcess();
        }
        public virtual void draw(SpriteBatch batch)
        {
            //draw this and all children
            if (visible)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].draw(batch);
                }
            }
            if (parent != null)
            {
                absolutePosition = parent.getPosition() + position;
            }
            else
            {
                absolutePosition = position;
            }
        }
        //function that allows our slides to move out of the way gracefullyt
        //also only make it move by its size
        public void transition(bool moveLeft = true)
        {
            if (!moved)
            {
                oldX = position.X;
                moved = true;
            }
            if (moveLeft && moved)
            {
                //slide off to the left
                Vector2 nPosition = position;
                nPosition.X -= 20;
                position = nPosition;
                if (position.X < (oldX - size.X))
                {
                    moved = false;
                    stop = true;
                }
            }
            else if (!moveLeft && moved)
            {
                //slide off to the right
                Vector2 nPosition = position;
                nPosition.X += 20;
                position = nPosition;
                if (position.X > (oldX + size.X))
                {
                    moved = false;
                    stop = true;
                }
            }
        }
        public bool stopTransition()
        {
            return stop;
        }
        public void resetTransition()
        {
            stop = false;
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
        public void setVisible(bool newVisible)
        {
            visible = newVisible;
        }

        public void addChild(UIElement newChild)
        {
            children.Add(newChild);
        }

        public void removeChild(UIElement child)
        {
            children.Remove(child);
            children.TrimExcess();
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
        public virtual E_UI_TYPES getUIType()
        {
            return type;
        }
        public bool getVisible()
        {
            return visible;
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
