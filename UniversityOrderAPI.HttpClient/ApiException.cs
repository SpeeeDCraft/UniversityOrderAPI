﻿namespace UniversityOrderAPI.HttpClient;

public class ApiException : Exception
{
    public ApiException(string message) : base(message) { }
}