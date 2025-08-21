namespace Management.Application.Messaging;

public interface ICommandHandler<in T> where T : ICommand;
