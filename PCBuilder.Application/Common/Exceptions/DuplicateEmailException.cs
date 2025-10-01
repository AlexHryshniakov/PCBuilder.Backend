namespace PCBuilder.Application.Common.Exceptions;

public class DuplicateException(string entityName,string propertyName, object key) 
    : Exception($"A {entityName} with this {propertyName} ({key}) already exists. ");

