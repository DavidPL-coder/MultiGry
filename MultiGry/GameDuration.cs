using System.Diagnostics;

namespace MultiGry
{
    class GameDuration
    {
        private Stopwatch Time;
        private const long MinuteInMilliseconds = 60000;

        public GameDuration() => 
            Time = new Stopwatch();

        public void Start() =>
            Time.Start();

        public void Stop() =>
            Time.Stop();

        public string GetTimeInTextVersion() => 
            Time.ElapsedMilliseconds < MinuteInMilliseconds ? GetSeconds() 
                                                            : GetMinutesAndSeconds();

        private string GetSeconds()
        {
            long Seconds = Time.ElapsedMilliseconds / 1000;
            return Seconds.ToString() + " s";
        }

        private string GetMinutesAndSeconds()
        {
            long Minutes = Time.ElapsedMilliseconds / MinuteInMilliseconds;
            long Seconds = Time.ElapsedMilliseconds % MinuteInMilliseconds / 1000;
            return Minutes.ToString() + " m " + Seconds.ToString() + " s";
        } 
    }
}
