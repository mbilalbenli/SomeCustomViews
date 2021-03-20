using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.SomeCustomViews.Shared.Helpers
{
    public class ColorFader
    {
        double interval = 750;

        /// <summary>
        /// Changes the background color of the "view".
        /// </summary>
        /// <param name="view"> To be changed background color. </param>
        /// <param name="toColor"> New color </param>
        /// <param name="speed"> Animation speed </param>
        public void BackgroundColorTo(View view, Color toColor, double speed)
        {
            if (speed<1)
            {
                interval *= speed;
            }

            var oldColor = view.BackgroundColor;

            var stepR = toColor.R - oldColor.R;            
            var stepG = toColor.G - oldColor.G;            
            var stepB = toColor.B - oldColor.B;

            int count = 0;
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                if (count % (interval / stepR) ==0)
                {
                    view.BackgroundColor = new Color(view.BackgroundColor.R + 1, view.BackgroundColor.G, view.BackgroundColor.B);
                }
                if (count % (interval / stepG) == 0)
                {
                    view.BackgroundColor = new Color(view.BackgroundColor.R, view.BackgroundColor.G+1, view.BackgroundColor.B);
                }
                if (count % (interval / stepB) == 0)
                {
                    view.BackgroundColor = new Color(view.BackgroundColor.R, view.BackgroundColor.G, view.BackgroundColor.B+1);
                }

                count++;
                if (count==interval)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            });

        }

        /// <summary>
        /// Changes the border color of the "view".
        /// </summary>
        /// <param name="view"> To be changed border color. </param>
        /// <param name="toColor"> New color </param>
        /// <param name="speed"> Animation speed </param>
        public void BorderColorTo(View view, Color toColor, double speed)
        {
            
            if (speed<1)
            {
                interval *= speed;
            }

            Color oldColor;
            if (view is Button button)
            {
                oldColor = button.BorderColor;

                var stepR = toColor.R - oldColor.R;
                var stepG = toColor.G - oldColor.G;
                var stepB = toColor.B - oldColor.B;

                int count = 0;
                Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                {
                    if (count % (interval / stepR) == 0)
                    {
                        button.BorderColor = new Color(button.BorderColor.R + 1, button.BorderColor.G, button.BorderColor.B);
                    }
                    if (count % (interval / stepG) == 0)
                    {
                        button.BorderColor = new Color(button.BorderColor.R, button.BorderColor.G + 1, button.BorderColor.B);
                    }
                    if (count % (interval / stepB) == 0)
                    {
                        button.BorderColor = new Color(button.BorderColor.R, button.BorderColor.G, button.BorderColor.B + 1);
                    }

                    count++;
                    if (count == interval)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                });
            }
            else if (view is Frame frame)
            {
                oldColor = frame.BorderColor;

                var stepR = toColor.R - oldColor.R;
                var stepG = toColor.G - oldColor.G;
                var stepB = toColor.B - oldColor.B;

                int count = 0;
                Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                {
                    if (count % (interval / stepR) == 0)
                    {
                        frame.BorderColor = new Color(frame.BorderColor.R + 1, frame.BorderColor.G, frame.BorderColor.B);
                    }
                    if (count % (interval / stepG) == 0)
                    {
                        frame.BorderColor = new Color(frame.BorderColor.R, frame.BorderColor.G + 1, frame.BorderColor.B);
                    }
                    if (count % (interval / stepB) == 0)
                    {
                        frame.BorderColor = new Color(frame.BorderColor.R, frame.BorderColor.G, view.BackgroundColor.B + 1);
                    }

                    count++;
                    if (count == interval)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                });
            }
            else
            {
                return;
            }

           

        }
    }
}
