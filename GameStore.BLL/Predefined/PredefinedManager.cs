using GameStore.DAL.Predefined.Dictionaries;
using GameStore.DAL.Predefined.Identity;

namespace GameStore.BLL.Predefined
{
    /// <summary>
    /// Точка входа к предзаданным значениям
    /// </summary>
    public class PredefinedManager
    {
        private PdRoles _roleNames;
        private PdGameKeyStatuses _gameKeyStatuses;

        /// <summary>
        /// Роли в системе
        /// </summary>
        public PdRoles AppRole
        {
            get
            {
                if(_roleNames is null)
                {
                    _roleNames = new PdRoles();
                }
                return _roleNames;
            }
        }

        /// <summary>
        /// Статусы ключа от игр
        /// </summary>
        public PdGameKeyStatuses GameKeyStatuses
        {
            get 
            {
                if(_gameKeyStatuses is null)
                {
                    _gameKeyStatuses = new PdGameKeyStatuses();
                }
                return _gameKeyStatuses;
            }
        }
    }
}
