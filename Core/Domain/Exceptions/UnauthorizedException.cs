﻿namespace Domain.Exceptions
{
    public class UnauthorizedException(string message="Invalid Email Or Password") : Exception(message);
}
