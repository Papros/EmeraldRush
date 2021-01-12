using EmeraldRush.Model.Tutorial;
using EmeraldRush.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.ViewModels.Instruction
{
    class InstructionViewModel : BaseViewModel
    {
        private List<TutorialPage> tutorialPanel;
        public List<TutorialPage> TutorialPanel
        {
            get
            {
                return tutorialPanel;
            }

            set
            {
                SetProperty(ref tutorialPanel, value); 
            }
        }

        public InstructionViewModel()
        {
            load();
        }

        private void load()
        {
            tutorialPanel = new List<TutorialPage>();
            tutorialPanel.Add(new TutorialPage("tutorial_gameui.png", "You will find all the necessary data on the main screen of the game. Current cave number (1), number of gems found and secured (2)"));
            tutorialPanel.Add(new TutorialPage("tutorial_gameui.png", "(4) the number of gems that can be obtained by leaving the cave at the moment, (5) the number of gems that have been abandoned"));
            tutorialPanel.Add(new TutorialPage("tutorial_gameui.png", "You can choose your decision by selecting one of button from decision panel (3)"));
        }
    }
}
