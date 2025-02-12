namespace GameStore.BLL.Infrastrcture
{
    /// <summary>
    /// Модель для вывода результата действия сервисов
    /// </summary>
    public class ResultServiceModel
    {
        /// <summary>
        /// Результат действия сервиса (true / false)
        /// </summary>
        public bool IsSucceeded { get; set; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string ErrorMes { get; set; }


        public long ObjId { get; set; }
    }
}
