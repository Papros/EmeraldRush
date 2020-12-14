using EmeraldRush.Model.FirebaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EmeraldRush.Views.Game
{
    class PlayerPanelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PlayerResting { get; set; }
        public DataTemplate PlayerExploring { get; set; }
        public DataTemplate PlayerDead { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            PlayersPublic player = ((PlayersPublic)item);

            switch (player.status)
            {
                case Model.GameEnum.PlayerStatus.DEAD: return this.PlayerDead;
                case Model.GameEnum.PlayerStatus.EXPLORING: return this.PlayerExploring;
                case Model.GameEnum.PlayerStatus.RESTING: return this.PlayerResting;
                default: return this.PlayerExploring;
            }

        }
    }
}
