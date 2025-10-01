using MediatR;
using PCBuidler.Domain.Shared.BlobStore;
using PCBuidler.Domain.Shared.Saga;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.ChangeAvatar;

public class ChangeAvatarCommandHandler:IRequestHandler<ChangeAvatarCommand,string>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPrefixProvider _prefixProvider; 
    private readonly IFileStorage _fileStorage;

    public ChangeAvatarCommandHandler(IUsersRepository usersRepository, IPrefixProvider prefixProvider, IFileStorage fileStorage)
    {
        _usersRepository = usersRepository;
        _prefixProvider = prefixProvider;
        _fileStorage = fileStorage;
    }


    public async Task<string> Handle(ChangeAvatarCommand request, CancellationToken ct)
    {
       var fileName= _prefixProvider.GetObjectPath(PrefixesOptions.UsersAvatar,request.UserId.ToString());
       var newUrl= _fileStorage.GetFileUrl(fileName);
       var currentUser = await _usersRepository.GetById(request.UserId, ct);
       
       var saga = new SagaOrchestrator();
       await saga.Execute(new List<SagaStep>
       {
           new (
               execute: () => _fileStorage.UploadFileAsync(
                   request.AvatarStream, fileName, request.ContentType, ct),
               compensate: () => _fileStorage.DeleteFileAsync(fileName, ct)
           ),
           
           new (
               execute: () => _usersRepository.UpdateAvatar(request.UserId, newUrl, ct),
               compensate: () => _usersRepository.UpdateAvatar(request.UserId, currentUser.AvatarUrl, ct)
           )
       });

       return newUrl;
    }
}


