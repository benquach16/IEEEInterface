﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    //graph class for getting input from arduino chip
    class UIGraph : UIElement
    {
        protected Texture2D bkg;
        protected int x, y;
        protected List<float> values;
        protected SpriteFont font;
        //so we can filter down the graph
        protected float maxValue;
        protected Texture2D t;
        public UIGraph(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, SpriteFont font, UIElement parent = null)
            : base(position, size, parent, E_UI_TYPES.UI_GRAPH)
        {
            this.values = new List<float>();
            this.font = font;
            this.bkg = new Texture2D(graphicsDevice, (int)size.X, (int)size.Y, false, SurfaceFormat.Color);
            Color[] colorData = new Color[(int)size.X * (int)size.Y];

            for (int i = 0; i < (int)size.X * (int)size.Y; i++)
            {
                //fill with temp colors for now
                //colorData[i] = Color.Gainsboro;
                if (i % size.X == 0 || i % size.X == size.X - 1)
                {
                    colorData[i] = new Color(200, 200, 200, 255);
                }
                else if (i < size.X || (i < size.X * size.Y) && i > size.X * (size.Y - 1))
                {
                    colorData[i] = new Color(200, 200, 200, 255);
                }
                else
                {
                    //int texColor = 255-(i / (2000));
                    int texColor = 32;
                    colorData[i] = new Color(texColor, texColor, texColor, 64);
                }
            }
            bkg.SetData(colorData);
            t = new Texture2D(graphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White });// fill the texture with white
        }

        public override void draw(SpriteBatch batch)
        {
            //for now, just draw like a box
            if (visible)
            {
                //draw lines for scaling?
                batch.Draw(bkg, absolutePosition, Color.White);
                batch.DrawString(font, "0", absolutePosition + new Vector2(0,size.Y), Color.White);
                batch.DrawString(font, "0", absolutePosition + new Vector2(-30, size.Y-30), Color.White);
                batch.DrawString(font, maxValue.ToString(), absolutePosition + new Vector2(-font.MeasureString(maxValue.ToString()).X, 0), Color.White);
                //and draw the lines
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i] > maxValue)
                        maxValue = values[i];
                    float v = size.Y * ((float)values[i] / maxValue);
                    Vector2 pos1 = getVectorFromPoint(i, v);
                    //we need to drawa up to the edge
                    if (i < values.Count - 1)
                    {
                        //if there is a next point
                        //we can draw a line between 'this' and the next one

                        float v2 = size.Y * ((float)values[i+1] / maxValue);
                        Vector2 pos2 = getVectorFromPoint(i + 1, v2);
                        if (pos2.X < size.X + absolutePosition.X && pos2.Y < size.Y + absolutePosition.Y)
                            drawLine(batch, pos1, pos2);

                    }
                    
                }
                if (values.Count > 200)
                {
                    values.RemoveAt(1);
                }
            }
            base.draw(batch);
        }

        public void update(float input)
        {
            //use this function to update the graph
            values.Add(input);
        }

        //function used by graph to draw lines
        protected void drawLine(SpriteBatch batch, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            batch.Draw(t, new Rectangle(// rectangle defines shape of line and position of start of line
                (int)start.X,
                (int)start.Y,
                (int)edge.Length(), //sb will strech the texture to fill this rectangle
                1), //width of line, change this to make thicker line
            null,
            Color.White, //colour of line
            angle,     //angle of line (calulated above)
            new Vector2(0, 0), // point in line about which to rotate
            SpriteEffects.None,
            0);
        }

        protected Vector2 getVectorFromPoint(int index, float value)
        {
            //aight so the purpose of this fnction is to
            //ensure that the vector2 created from this value fits in the graph and isnt autistic
            Vector2 ret = new Vector2(absolutePosition.X+(index*8), absolutePosition.Y+size.Y-value);
            return ret;
        }
    }
}
