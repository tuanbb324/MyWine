using WebCore.Infrastructures.Repositories;

namespace WebCore.Infrastructures.Services
{
    public abstract class BaseServiceV2
    {
        public IUnitOfWork _unitOfWork { get; set; }

        public BaseServiceV2(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}