using GameStore.BLL.Predefined.ConstPredefineds;
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
        private PdGameDevelopers _gameDevelopers;
        private PdGameLabels _gameLabels;
        private PdGenres _genres;
        private PdTypesSelectedGames _typesSelectedGames;
        private PdPaymentMethods _paymentMethods;
        private PdGamePageParts _gamePageParts;


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

        public PdGameDevelopers GameDevelopers
        {
            get
            {
                if (_gameDevelopers is null)
                {
                    _gameDevelopers = new PdGameDevelopers();
                }
                return _gameDevelopers;
            }
        }

        public PdGameLabels GameLabels
        {
            get
            {
                if ( _gameLabels is null)
                {
                   _gameLabels = new PdGameLabels();
                }
                return _gameLabels;
            }
        }

        public PdGenres Genres
        {
            get
            {
                if( _genres is null)
                {
                    _genres = new PdGenres();
                }
                return _genres;
            }
        }

        public PdTypesSelectedGames SelectedGames
        {
            get 
            {
                if(_typesSelectedGames is null)
                {
                    _typesSelectedGames = new PdTypesSelectedGames();
                }
                return _typesSelectedGames;
            }
        }

        public PdPaymentMethods PaymentMethods
        {
            get
            {
                if(_paymentMethods is null)
                {
                    _paymentMethods = new PdPaymentMethods();
                }
                return _paymentMethods;
            }
        }

        public PdGamePageParts GamePageParts
        {
            get
            {
                if (_gamePageParts is null)
                {
                    _gamePageParts = new PdGamePageParts();
                }
                return _gamePageParts;
            }
        }


    }
}
