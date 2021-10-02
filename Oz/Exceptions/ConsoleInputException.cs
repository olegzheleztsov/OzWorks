// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Exceptions;

public class ConsoleInputException : Exception
{
    public ConsoleInputException(string message): base(message){}
}