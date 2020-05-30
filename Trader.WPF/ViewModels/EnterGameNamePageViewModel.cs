using GalaSoft.MvvmLight.Command;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.WPF.Models;
using WPF.Common.Infrastructure;

namespace Trader.WPF.ViewModels
{
    class EnterGameNamePageViewModel
    {
        #region Private Definitions
        IEventAggregator m_eventAggregator;

        IGenericService<Game, GameDto, int> m_gamesService;
        #endregion

        public EnterGameNamePageViewModel(IEventAggregator eventAggregator)
        {
            InitCommands();
            InitServices();

            m_eventAggregator = eventAggregator;
        }

        public string GameName { get; set; }

        public ICommand CreateGameCommand { get; set; }

        void InitCommands()
        {
            CreateGameCommand = new RelayCommand(CreateGameAsync);
        }
        void InitServices()
        {
            m_gamesService = MyContainer.Resolve<IGenericService<Game, GameDto, int>>();
        }

        async void CreateGameAsync()
        {
            var game = new GameDto
            {
                GameName = this.GameName,
                CurrentMoveNumber = 1,
                Date = DateTime.Now
            };
            GameDto insertedGame = await Task.Run(() => m_gamesService.AddOrUpdate(game));

            m_eventAggregator.GetEvent<GameIdEvent>().Publish(insertedGame.GameId);
        }
    }
}
