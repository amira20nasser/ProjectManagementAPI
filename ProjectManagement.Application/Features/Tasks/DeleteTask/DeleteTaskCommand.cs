using MediatR;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Tasks.DeleteTask
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteTaskCommand(Guid id) => Id = id;
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IGenericRepo<TaskItem, Guid> _taskRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskCommandHandler(IGenericRepo<TaskItem, Guid> taskRepo, IUnitOfWork unitOfWork)
        {
            _taskRepo = taskRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _taskRepo.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"Task with Id {request.Id} not found.");

            _taskRepo.Delete(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
