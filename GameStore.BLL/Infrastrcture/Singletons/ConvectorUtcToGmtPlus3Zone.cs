

namespace GameStore.BLL.Infrastrcture.Singletons
{
    public sealed class ConvectorUtcToGmtPlus3Zone
    {
        //объект клсса 
        private static ConvectorUtcToGmtPlus3Zone _instance;
        private static readonly object _lock = new object();

        private ConvectorUtcToGmtPlus3Zone() { }

        //Это статический метод, управляющий доступом к экземпляру.
        public static ConvectorUtcToGmtPlus3Zone GetInstance()
        {
            // Проверяем, существует ли уже экземпляр
            if (_instance is null)
            {
                // Блокируем доступ к коду ниже, чтобы предотвратить создание
                // нескольких экземпляров в многопоточной среде
                lock (_lock)
                {
                    // Проверяем снова, чтобы убедиться, что экземпляр все еще null
                    if (_instance is null)
                    {
                        _instance = new ConvectorUtcToGmtPlus3Zone();
                    }
                }
            }
            return _instance;
        }
        public DateTime ConvectorDate(DateTime sourceDate)
        {
            TimeZoneInfo gmtPlus3Zone = TimeZoneInfo.FindSystemTimeZoneById("Belarus Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(sourceDate, gmtPlus3Zone);
        }
    }
}
