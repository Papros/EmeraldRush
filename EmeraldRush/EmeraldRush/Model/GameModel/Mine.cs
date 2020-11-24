﻿using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.GameModel
{
    class Mine
    {
        public int[] Node { get; set; }
        public int EmeraldsForTake { get; set;}
        public MineStatus MineState { get; set; }
        public int LastMoveTimestamp { get; set; }

        public Mine( )
        {
            this.MineState = MineStatus.NOT_VISITED; 
            this.Node = new int[0];
            this.EmeraldsForTake = 0;
            this.LastMoveTimestamp = 0;
        }

    }
}