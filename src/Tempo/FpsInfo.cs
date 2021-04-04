namespace Tempo
{
    public struct FpsInfo
    {
        private double _timeTotal;
        private double _time1Sec;
        private double _time3Sec;
        private long _frameCntTotal;
        private long _frameCnt1Sec;
        private long _frameCnt3Sec;

        private double _preFpsIn1Sec;
        private double _preFpsIn3Sec;

        public string GetFpsInfo() => $"{_preFpsIn1Sec:F2}(1s) {_preFpsIn3Sec:F2}(3s) {_frameCntTotal / _timeTotal:F2}(total)";

        public bool Update(double time) 
        {
            var bUpdate = false;
            _timeTotal += time;
            _time1Sec += time;
            _time3Sec += time;

            _frameCntTotal++;
            _frameCnt1Sec++;
            _frameCnt3Sec++;

            if (1.0f <= _time1Sec) 
            {
                _preFpsIn1Sec = this._frameCnt1Sec / _time1Sec;
                _time1Sec = 0;
                _frameCnt1Sec = 0;
                bUpdate = true;
            }
            if (3.0f <= _time3Sec) 
            {
                _preFpsIn3Sec = _frameCnt3Sec / _time3Sec ;
                _time3Sec = 0;
                _frameCnt3Sec = 0;
                bUpdate = true;
            }
            return bUpdate;
        }
    }
}