using System;
using CRMFocus.Entity;
using CRMFocus.Common;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;

namespace CRMFocus.Business.Implementations
{
    public class DummyService : BaseService<Dummy>, IDummyService
    {
        private readonly IDummyRepository _dummyRepository;

        public DummyService(IDummyRepository dummyRepository)
            : base(dummyRepository)
        {
            _dummyRepository = dummyRepository;
        }

        public string GetRoleDummy()
        {
            throw new NotImplementedException();
        }
    }
}
