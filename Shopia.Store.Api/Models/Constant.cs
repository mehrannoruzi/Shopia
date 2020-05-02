namespace Shopia.Store.Api
{
    public class Constant
    {
        public long OrderId { set; get; }
        public string TransId { set; get; }
        private static Constant _ins;
        private Constant()
        {

        }
        public static Constant Instance()
        {
            if (_ins == null)
            {
                _ins = new Constant();
                return _ins;
            }
            else return _ins;
        }

    }
}
