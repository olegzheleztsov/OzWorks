using System;

namespace Oz.Rob;

public record EightQueenResult(bool[,] Cells, bool Found, TimeSpan TimeTaken, int PositionChecked);