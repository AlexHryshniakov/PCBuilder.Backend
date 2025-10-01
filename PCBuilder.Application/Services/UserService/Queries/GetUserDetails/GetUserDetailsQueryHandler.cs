using AutoMapper;
using MediatR;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Queries.GetUserDetails;

public class GetUserDetailsQueryHandler:IRequestHandler<GetUserDetailsQuery, GetUserDetailsQueryVm>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUserDetailsQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<GetUserDetailsQueryVm> Handle(GetUserDetailsQuery request, CancellationToken ct)
    {
       var user= await _usersRepository.GetById(request.UserId,ct);
       
       return _mapper.Map<GetUserDetailsQueryVm>(user);
    }
}