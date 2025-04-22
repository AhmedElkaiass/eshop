namespace BuildingBlocks.Exceptions;

public class InternalServerErrorException(string message) : Exception(message);
public class BadRequestException(string message) : Exception(message);
