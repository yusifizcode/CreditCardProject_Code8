﻿namespace TechnestHackhaton.Application.Exceptions;

public class AuthorizationException : Exception
{

    public AuthorizationException(string message) : base(message)
    {
    }
}