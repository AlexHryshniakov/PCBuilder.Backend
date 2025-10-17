using MediatR;
using PCBuilder.Core.Shared.BlobStore;
using PCBuilder.Core.Shared.Saga;
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
        var fileName = _prefixProvider
            .GetObjectPath(PrefixesOptions.UsersAvatar, request.UserId.ToString());
        var tempFileName =
            _prefixProvider.GetTempObjectPath(PrefixesOptions.UsersAvatar, request.UserId.ToString());

        var newUrl = _fileStorage.GetFileUrl(fileName);

        var currentUser = await _usersRepository.GetById(request.UserId, ct);

        var saga = new SagaOrchestrator();

        await using (request)
        {
            await saga.Execute(new List<SagaStep>()
                .AddStep(new(
                        execute: () =>
                            _fileStorage.UploadFileAsync(request.AvatarStream, tempFileName, request.ContentType, ct),
                        compensate: () =>
                            _fileStorage.DeleteFileAsync(tempFileName, ct)),
                    condition: true
                )
                .AddStep(new(
                        execute: () =>
                            _usersRepository.UpdateAvatar(request.UserId, newUrl, ct),
                        compensate: ()
                            => _usersRepository.UpdateAvatar(request.UserId, currentUser.AvatarUrl, ct)),
                    condition: !String.Equals(currentUser.AvatarUrl, newUrl))
            );

            await _fileStorage.CopyFileAsync(tempFileName, fileName, ct);
            await _fileStorage.DeleteFileAsync(tempFileName, ct);
            return newUrl;
        }
    }
}