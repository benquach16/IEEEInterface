using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WinFormsGraphicsDevice
{
    //for tempurature slide
    //for weather

    enum E_WEATHER_STATES
    {
        WEATHER_CLOUDY,
        WEATHER_SUNNY,
        WEATHER_DRIZZLE,
        WEATHER_THUNDERSTORMS,
        WEATHER_RAIN
    };
    class SlideTemp : Slide
    {

        protected UIImage weatherImage;
        E_WEATHER_STATES weatherState;
        public SlideTemp(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) : 
            base(graphicsDevice, uiManager, Content, position, size)
        {
            this.weatherState = E_WEATHER_STATES.WEATHER_CLOUDY;
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), "Weather - Tempurature", font, this);
            weatherImage = uiManager.addImage(new Vector2(400, 20), Content.Load<Texture2D>("Cloudy"), this);
            uiManager.addStaticText(new Vector2(20, 190), new Vector2(200, 200), "666 C", fontSize48, this);
        }
        ~SlideTemp()
        {
        }
        public override void run()
        {
            //if we need to run anything here
            setWeatherPictures();

        }
        //handle the changing of icons for weataher here
        protected void setWeatherPictures()
        {
            switch (weatherState)
            {
                case E_WEATHER_STATES.WEATHER_CLOUDY:
                    {
                        weatherImage.setImage(Content.Load<Texture2D>("Cloudy"));
                        break;
                    }
                case E_WEATHER_STATES.WEATHER_DRIZZLE:
                    {
                        weatherImage.setImage(Content.Load<Texture2D>("Slight Drizzle"));
                        break;
                    }
                case E_WEATHER_STATES.WEATHER_RAIN:
                    {
                        weatherImage.setImage(Content.Load<Texture2D>("Drizzle"));
                        break;
                    }
                case E_WEATHER_STATES.WEATHER_SUNNY:
                    {
                        weatherImage.setImage(Content.Load<Texture2D>("Sunny"));
                        break;
                    }
                case E_WEATHER_STATES.WEATHER_THUNDERSTORMS:
                    {
                        weatherImage.setImage(Content.Load<Texture2D>("Thunderstorms"));
                        break;
                    }
            }

        }
    }
}
