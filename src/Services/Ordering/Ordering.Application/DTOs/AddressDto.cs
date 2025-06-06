﻿namespace Ordering.Application.DTOs;

public record AddressDto(string FirstName, string LastName, string Email, 
    string AddressLine, string City, string State, string ZipCode, string Country);
