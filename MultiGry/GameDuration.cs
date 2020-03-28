using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class GameDuration
    {
        private Stopwatch GameTime;
        private const long MinuteInMilliseconds = 60000;

        public GameDuration() => 
            GameTime = new Stopwatch();

        public void Start() =>
            GameTime.Start();

        public void Stop() =>
            GameTime.Stop();

        public string GetTimeInTextVersion() => 
            GameTime.ElapsedMilliseconds < MinuteInMilliseconds ? GetSeconds() : GetMinutesAndSeconds();

        private string GetSeconds()
        {
            long Seconds = GameTime.ElapsedMilliseconds / 1000;
            return Seconds.ToString() + " s";
        }

        private string GetMinutesAndSeconds()
        {
            long Minutes = GameTime.ElapsedMilliseconds / MinuteInMilliseconds;
            long Seconds = GameTime.ElapsedMilliseconds % MinuteInMilliseconds / 1000;
            return Minutes.ToString() + " m " + Seconds.ToString() + " s";
        } 
    }
}
