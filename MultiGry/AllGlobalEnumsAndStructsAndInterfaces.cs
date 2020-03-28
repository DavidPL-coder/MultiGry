using System;
using System.Collections.Generic;

namespace MultiGry
{
    public enum OptionsCategory
    {
        NotSelectedYet, NormalOption, Wrong, ExitTheProgram, CanceledExit
    }

    public interface IMenuOption
    {
        OptionsCategory OptionExecuting();
        string NameOption { get; }
    }

    public struct Rect
    {
        public int Left, Top, Right, Bottom;
    }

    public enum MinesweeperGameStatus
    {
        DuringGame, PlayerLost, PlayerWin, Break
    }
}