// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.Exceptions;

public class InputFinishedException : ConsoleInputException
{
    public InputFinishedException(string message) : base(message)
    {
    }
}