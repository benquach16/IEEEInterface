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
        float currentTemp;
        UIStaticText tempText;
        UIStaticText radText;
        UIStaticText humidityText;
        //UIWindow yesterdaysTemp;
        public SlideTemp(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) : 
            base(graphicsDevice, uiManager, Content, position, size)
        {
            this.currentTemp = 0;
            this.weatherState = E_WEATHER_STATES.WEATHER_CLOUDY;
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), "Weather - Tempurature", font, this);

            tempText = uiManager.addStaticText(new Vector2(20, 190), new Vector2(200, 200), "Today's tempurature is 666 F", fontSize48, this);
            radText = uiManager.addStaticText(new Vector2(20, 290), new Vector2(200, 200), "The Solar Radiation level is 500 watts per square meter", small, this);
            humidityText = uiManager.addStaticText(new Vector2(20, 370), new Vector2(200, 200), "The Relative Humidity is 77%", fontSize32, this);
            uiManager.addStaticText(new Vector2(20, 470), new Vector2(200, 200), "It is currently CLOUDY", fontSize48, this);
            weatherImage = uiManager.addImage(new Vector2(850, 400), Content.Load<Texture2D>("Cloudy"), this);
            
            
            
            //yesterdaysTemp = uiManager.addWindow(new Vector2(20, 600), new Vector2(400, 400), this);
            //uiManager.addStaticText(new Vector2(10, 10), new Vector2(200, 200), "Yesterday's weather", fontSize32, yesterdaysTemp);
        }
        ~SlideTemp()
        {
        }
        public override void run()
        {
            //if we need to run anything here
            setWeatherPictures();

        }
        public void update(float newTmp, float newRad, float newHumid)
        {
            currentTemp = newTmp;
            tempText.setText("Today's tempurature is "+newTmp.ToString() + " F");
            radText.setText("The Solar Radiation Level is " + newRad.ToString() + " watts per square meter");
            humidityText.setText("The Relative Humidity is " + newHumid.ToString() + " %");
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
