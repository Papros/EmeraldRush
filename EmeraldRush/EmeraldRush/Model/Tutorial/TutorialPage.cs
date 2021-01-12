using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.Tutorial
{
    class TutorialPage
    {
        public string ImagePath { get; set; }
        public string TipText { get; set; }

        public TutorialPage(string imagePath, string tipText)
        {
            this.ImagePath = imagePath;
            this.TipText = tipText;
        }
    }
}
