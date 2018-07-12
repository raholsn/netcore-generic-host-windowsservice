using Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application
{
    public class CleanupService : ICleanupService
    {
        private readonly ICleanupRepository _repository;

        public CleanupService(ICleanupRepository repository)
        {
            _repository = repository;
        }

        public async Task CleanUp()
        {
            await _repository.CleanUp(new Domain.CleanUp(Guid.NewGuid(), DateTime.UtcNow));
        }
    }
}
